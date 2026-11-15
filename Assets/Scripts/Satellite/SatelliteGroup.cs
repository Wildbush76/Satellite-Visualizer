using System.Collections.Generic;
using System.IO;
using System.Xml;
using Assets.Scripts.Sgp4Management;
using One_Sgp4;
using One_Sgp4.omm;
using UnityEngine;

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
            List<Omm> ommList = ParserOMM.Parse(ommDoc);

            foreach (Omm omm in ommList)
            {
                CreateSatellite(omm);
            }
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
            UpdateSatellites();
        }

        void Start()
        {
            world = World.Instance;
        }
        public void UpdateSatellites()
        {
            foreach (Satellite satellite in satellites)
            {
                satellite.UpdatePosition(world.Now, world.viewerLocation);
            }
        }

    }
}