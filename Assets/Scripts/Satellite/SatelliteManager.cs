using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Satellite
{

    public class SatelliteManager : MonoBehaviour
    {
        public static SatelliteManager Instance { get; private set; }


        [SerializeField]
        private GameObject satelliteGroupPrefab;

        public readonly Dictionary<string, SatelliteConstellation> satelliteGroups = new();


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

        private void CreateGroup(SatelliteGroupConfig type)
        {
            var obj = GameObject.Instantiate(satelliteGroupPrefab, transform);
            SatelliteConstellation group = obj.GetComponent<SatelliteConstellation>();
            group.Initialize(type);

            if (!satelliteGroups.TryAdd(type.CelestrakId, group))
            {
                Debug.LogWarning(string.Format("A satellite group with the type %s already exists!", type.ToString()), obj);
            }
        }

        private void LoadGroups()
        {
            foreach (var groupList in SatelliteGroupConfig.ALL_GROUPS.Values)
            {
                groupList.ForEach(CreateGroup);
            }
        }

        void Start()
        {
            LoadGroups();
        }


    }
}