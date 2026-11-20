using System.Collections.Generic;
using System.IO;
using System.Xml;
using Assets.Scripts.Sgp4Management;
using UnityEngine;
using Zeptomoby.OrbitTools;

namespace Assets.Scripts.Satellite
{
    public class SatelliteGroup : MonoBehaviour
    {
        private readonly List<Satellite> satellites = new();

        [SerializeField]
        private GameObject satellitePrefab;
        private World world;

        public bool Active
        {
            get => active; set
            {
                gameObject.SetActive(active);
                active = value;
            }
        }

        private bool active;

        public void Initialize(string ommPath)
        {
            XmlDocument ommDoc = new();
            ommDoc.Load(ommPath);

            List<Omm> OrbitMeanMessages = Omm.Parse(ommDoc);
            OrbitMeanMessages.ForEach(CreateSatellite);
        }

        private void CreateSatellite(Omm omm)
        {
            var newSatelliteObj = Instantiate(satellitePrefab, transform);
            var newSatellite = newSatelliteObj.GetComponent<Satellite>();

            newSatellite.Initialize(omm);
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