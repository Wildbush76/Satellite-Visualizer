using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using Assets.Scripts.Celestrak;
using UnityEngine;
using Zeptomoby.OrbitTools;

namespace Assets.Scripts.Satellite
{
    public class SatelliteConstellation : MonoBehaviour
    {
        private readonly List<Satellite> satellites = new();

        [SerializeField]
        private GameObject satellitePrefab;
        private World world;
        private SatelliteTypes type;

        public bool Active
        {
            get => active; set
            {
                gameObject.SetActive(active);
                active = value;
            }
        }

        private bool active;

        private void LoadOmmFile(string file)
        {
            XmlDocument ommDoc = new();
            ommDoc.Load(file);

            List<Omm> OrbitMeanMessages = Omm.Parse(ommDoc);
            OrbitMeanMessages.ForEach(CreateSatellite);
        }

        public void Initialize(SatelliteTypes group)
        {
            type = group;
            if (File.Exists(group.OmmFile))
            {
                LoadOmmFile(group.OmmFile);
            }
            else
            {
                _ = DownloadThenLoad();
            }

        }

        private async Awaitable DownloadThenLoad()
        {
            Debug.Log("Downloading file from celestrak");
            bool success = await Downloader.Download(type.CelestrakQuery, type.OmmFile);
            if (success)
            {
                Debug.Log("Download successful");
                LoadOmmFile(type.OmmFile);
            }
            else
                Debug.LogError("Failed to download file");
        }

        private void CreateSatellite(Omm omm)
        {
            var newSatelliteObj = Instantiate(satellitePrefab, transform);
            var newSatellite = newSatelliteObj.GetComponent<Satellite>();

            newSatellite.Initialize(omm);
            newSatellite.Color = type.Color;

            satellites.Add(newSatellite);
        }

        public void Update()
        {
            UpdateSatellites(world.WorldTimeJulian, world.ViewerPositionECI);
        }

        void Start()
        {
            world = World.Instance;
        }
        public void UpdateSatellites(Julian time, EciTime viewerLocation)
        {
            satellites.ForEach((s) => s.UpdatePosition(time, viewerLocation));
        }

    }
}