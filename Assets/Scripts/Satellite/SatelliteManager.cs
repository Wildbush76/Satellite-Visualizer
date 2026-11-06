using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using One_Sgp4;
using UnityEngine;

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
            group.Initialize(tlePath, type);

            if (!satelliteGroups.TryAdd(type, group))
            {
                Debug.LogWarning(string.Format("A satellite group with the type %s already exists!", type.ToString()), obj);
            }
        }
        void Start()
        {
            //TESTING
            CreateGroup(Path.Combine(Application.streamingAssetsPath, "gp.txt"), SatelliteType.GNSS);
            //TESTING
        }


    }
}