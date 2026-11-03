using System;
using System.IO;
using System.Net;
using One_Sgp4;
using UnityEngine;

namespace Assets.Scripts.Satellite
{

    public class SatelliteManager : MonoBehaviour
    {
        private const String CelestrakURL = "https://celestrak.org/NORAD/elements/supplemental/sup-gp.php?SOURCE={0}[&FORMAT=JSON]";

        public SatelliteManager Instance { get; private set; }

        [SerializeField]
        private GameObject satellitePrefab;


        private void Awake()
        {
            if (Instance != null && Instance != null)
            {
                Debug.LogWarning("More than instance of a satelliteManager found. Destroying extra");
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }


        private void CreateSatellite(Tle tle)
        {
            var newSatelliteObj = Instantiate(satellitePrefab, transform);
            var newSatellite = newSatelliteObj.GetComponent<Satellite>();

            newSatellite.Initialize(tle);

        }

        private void LoadTLE()
        {

        }

        void Start()
        {
            //TESTING
            string path = Path.Combine(Application.streamingAssetsPath, "gp.txt");
            foreach (Tle tle in ParserTLE.ParseFile(path))
            {
                CreateSatellite(tle);
            }
            //TESTING
        }

        void Update()
        {
            World world = World.Instance;

            foreach (var satellite in Satellite.satellites.Values)
            {
                if (satellite.NeedsUpdate)
                {
                    satellite.UpdatePosition(world.Now, world.viewerLocation);

                }
            }
        }


    }
}