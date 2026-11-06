using System;
using System.Collections;
using System.Runtime.InteropServices;
using One_Sgp4;
using UnityEngine;
using UnityEngine.Android;

namespace Assets.Scripts
{
    public class World : MonoBehaviour
    {
        public static World Instance { get; private set; }

        [NonSerialized]
        public Coordinate viewerLocation = new Coordinate(32.99189751954053, -96.75764567626766);

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

        private void InitGPS()
        {

#if MOBILE
            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                PermissionCallbacks callback = new();
                callback.PermissionGranted += PermissionGrantedCallback;

                Permission.RequestUserPermission(Permission.FineLocation);
            }

#endif
        }

        private void PermissionGrantedCallback(string permission)
        {

        }

    }
}