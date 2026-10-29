

using System.IO;
using One_Sgp4;
using UnityEngine;

namespace Assets.Scripts.Satellite
{
    public class TLEManager
    {
        public string TleDir { get => Path.Combine(Application.persistentDataPath, TleDirName); }
        private const string TleDirName = "TLEs";

        private void EnsurePath(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }


    }
}