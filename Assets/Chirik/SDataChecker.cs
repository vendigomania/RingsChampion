using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;

namespace Services.S
{
    [Serializable]
    public class SDataChecker
    {
        [SerializeField] private string serviceAddress;

        [Header("y/m/d"), SerializeField] private int[] dateValues = new int[3];

        public async Task<bool> NeedCheckServer()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                return false;
            }

            try
            {
                using (WebClient client = new WebClient())
                {
                    var response = await client.DownloadStringTaskAsync(new Uri(serviceAddress));

                    var mills = JObject.Parse(response).Property("time").Value.ToObject<long>();

                    return
                        new DateTime(1970, 1, 1).AddMilliseconds(mills) > new DateTime(dateValues[0], dateValues[1], dateValues[2]);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
