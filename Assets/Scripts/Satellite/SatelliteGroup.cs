using System.Collections.Generic;
using System.IO;
using One_Sgp4;
using UnityEngine;

namespace Assets.Scripts.Satellite
{
    public class SatelliteGroup : MonoBehaviour
    {
        private readonly List<Satellite> satellites = new();

        [SerializeField]
        private GameObject satellitePrefab;
        private World world;
        private SatelliteType type;

        public bool Active
        {
            get => active; set
            {
                gameObject.SetActive(active);
                active = value;
            }
        }

        private bool active;

        public void Initialize(string tlePath, SatelliteType type)
        {
            try
            {

                foreach (Tle tle in ParserTLE.ParseFile(tlePath))
                {
                    CreateSatellite(tle);
                }
                this.type = type;
            }
            catch (FileNotFoundException)
            {
                Debug.LogError("Failed to load TLE file at " + type, gameObject);
            }
        }

        private void CreateSatellite(Tle tle)
        {
            var newSatelliteObj = Instantiate(satellitePrefab, transform);
            var newSatellite = newSatelliteObj.GetComponent<Satellite>();

            newSatellite.Initialize(tle, type);
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