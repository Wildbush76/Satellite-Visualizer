using System;
using One_Sgp4;
using UnityEngine;

namespace Assets.Scripts
{
    public class World : MonoBehaviour
    {
        public static World Instance { get; private set; }

        [NonSerialized]
        public Coordinate viewerLocation = new Coordinate(-47.81, -175.44);

        public EpochTime Now { get; private set; } = new EpochTime(DateTime.Now);

        private void Awake()
        {
            if (Instance != null && Instance != null)
            {
                Debug.LogWarning("More than instance of World found. Destroying extra");
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }


        private void Update()
        {

        }
    }
}