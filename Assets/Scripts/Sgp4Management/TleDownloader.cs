using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Sgp4Management
{
    public class TleLoader
    {
        private const string CELESTRAK_URL = "https://celestrak.org/NORAD/elements/supplemental/sup-gp.php?{0}={1}&FORMAT={2}";
        private const string JSON_FORMAT = "json";


        public static string GetCelestrakUrl(string query, string value, string format)
        {
            return String.Format(CELESTRAK_URL, query, value, format);
        }

        public static async Awaitable<bool> GetJsonTLEGroup(string group, string path)
        {
            string url = GetCelestrakUrl(CelestrakQueries.GROUP.ToString(), group, JSON_FORMAT);
            return await Download(url, path);
        }
        public static async Awaitable<bool> Download(String url, String targetPath)
        {
            using (UnityWebRequest request = new(url, UnityWebRequest.kHttpVerbGET))
            {
                request.downloadHandler = new DownloadHandlerFile(targetPath);

                await Awaitable.FromAsyncOperation(request.SendWebRequest());


                if (request.result == UnityWebRequest.Result.Success)
                    return true;
            }

            return false;
        }
    }
}