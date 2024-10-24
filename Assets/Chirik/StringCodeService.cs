using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

namespace Services
{
    public static class StringCodeService
    {
        private const int CryptoKeySize = 128;

        private const int DerivationIterationsCount = 1000;

        public static string CryptSwitch(string text, string passPhrase, bool isEncrypt)
        {
            if(isEncrypt)
            {
                return Encrypt(text, passPhrase);
            }
            else
            {
                return Decrypt(text, passPhrase);
            }
        }

        public static string Encrypt(string text, string passText)
        {
            var saltEntropyBytes = Generate256BitsOfRandomEntropy();
            
            using (var password = new Rfc2898DeriveBytes(passText, saltEntropyBytes, DerivationIterationsCount))
            {
                var keyBytes = password.GetBytes(CryptoKeySize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 128;
                    symmetricKey.Mode = CipherMode.ECB;
                    symmetricKey.Padding = PaddingMode.ISO10126;

                    var fourStringBytes = Generate256BitsOfRandomEntropy();
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, fourStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                var cryptoTextBytes = saltEntropyBytes;
                                var encryptTextBytes = Encoding.UTF8.GetBytes(text);

                                cryptoStream.Write(encryptTextBytes, 0, encryptTextBytes.Length);
                                cryptoStream.FlushFinalBlock();

                                cryptoTextBytes = cryptoTextBytes.Concat(fourStringBytes).ToArray();
                                cryptoTextBytes = cryptoTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cryptoTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(string text, string passText)
        {
            var cryptoTextBytesWithSaltAndIv = Convert.FromBase64String(text);
            var saltEntropyBytes = cryptoTextBytesWithSaltAndIv.Take(CryptoKeySize / 8).ToArray();

            using (var password = new Rfc2898DeriveBytes(passText, saltEntropyBytes, DerivationIterationsCount))
            {
                var keyBytes = password.GetBytes(CryptoKeySize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 128;
                    symmetricKey.Mode = CipherMode.ECB;
                    symmetricKey.Padding = PaddingMode.ISO10126;
                    
                    var fourStringBytes = cryptoTextBytesWithSaltAndIv.Skip(CryptoKeySize / 8).Take(CryptoKeySize / 8).ToArray();
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, fourStringBytes))
                    {
                        var cipherTextBytes = cryptoTextBytesWithSaltAndIv.Skip((CryptoKeySize / 8) * 2).Take(cryptoTextBytesWithSaltAndIv.Length - ((CryptoKeySize / 8) * 2)).ToArray();
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            using (var streamReader = new StreamReader(cryptoStream, Encoding.UTF8))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomArrayBytes = new byte[16];
            using (var rangeCsp = new RNGCryptoServiceProvider())
            {
                rangeCsp.GetBytes(randomArrayBytes);
                return randomArrayBytes;
            }
        }
    }
}
