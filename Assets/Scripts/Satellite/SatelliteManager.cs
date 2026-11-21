using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Satellite
{

    public class SatelliteManager : MonoBehaviour
    {
        public SatelliteManager Instance { get; private set; }


        [SerializeField]
        private GameObject satelliteGroupPrefab;

        private readonly Dictionary<string, SatelliteConstellation> satelliteGroups = new();


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

        private void CreateGroup(SatelliteTypes type)
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
            SatelliteTypes.ALL_GROUPS.ForEach(CreateGroup);
        }

        void Start()
        {
            LoadGroups();
        }


    }
}