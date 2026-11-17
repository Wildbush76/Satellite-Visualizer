using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using AFSPC.AstroStds.Wrappers;

namespace Assets.Scripts.Satellite
{

    public class SatelliteManager : MonoBehaviour
    {
        public SatelliteManager Instance { get; private set; }


        [SerializeField]
        private GameObject satelliteGroupPrefab;

        private readonly Dictionary<SatelliteType, SatelliteGroup> satelliteGroups = new();


        private void Awake()
        {
            //ASTRO LIB TESTS

            long satKey = TleWrapper.TleAddSatFrLines("1 90021U RELEAS14 00051.47568104 +.00000184 +00000+0 +00000-4 0 0814",
                                              "2 90021   0.0222 182.4923 0000720  45.6036 131.8822  1.00271328 1199");




            Debug.Log($"Sat key = {satKey}");

            ///ASTRO LIB TESTS






            if (Instance != null && Instance != null)
            {
                Debug.LogWarning("More than instance of a satelliteManager found. Destroying extra");
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void CreateGroup(string tlePath, SatelliteType type)
        {
            var obj = GameObject.Instantiate(satelliteGroupPrefab, transform);
            SatelliteGroup group = obj.GetComponent<SatelliteGroup>();
            group.Initialize(tlePath);

            if (!satelliteGroups.TryAdd(type, group))
            {
                Debug.LogWarning(string.Format("A satellite group with the type %s already exists!", type.ToString()), obj);
            }
        }
        void Start()
        {
            //TESTING
            CreateGroup(Path.Combine(Application.streamingAssetsPath, "galileo.xml"), SatelliteType.GNSS);
            //TESTING
        }


    }
}