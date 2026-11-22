using System;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Celestrak
{
    public static class Downloader
    {
        private const string CELESTRAK_URL = "https://celestrak.org/NORAD/elements/gp.php?{0}={1}&FORMAT={2}";

        public static string GetCelestrakUrl(string query, string value, string format)
        {
            return String.Format(CELESTRAK_URL, query, value, format);
        }
        public static async Awaitable<bool> Download(String url, String targetPath)
        {
            Debug.Log($"Downloading from URL {url}");
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                request.downloadHandler = new DownloadHandlerFile(targetPath);
                request.timeout = 15;
                await request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                    return true;
                Debug.LogWarning($"Download Failed, Code => {request.responseCode}, Response = {request.result.ToString()}");
            }

            return false;
        }
    }
}