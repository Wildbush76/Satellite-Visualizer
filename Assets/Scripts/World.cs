using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Android;
using Zeptomoby.OrbitTools;

namespace Assets.Scripts
{
    public class World : MonoBehaviour
    {
        public static World Instance { get; private set; }

        public DateTime WorldTime { get; private set; } = DateTime.UtcNow;
        public Julian WorldTimeJulian { get => new Julian(WorldTime); }
        public Site Site { get; private set; } = new Site(0, 0, 0);
        public EciTime ViewerPositionECI
        {
            get => Site.PositionEci(WorldTimeJulian);
        }

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
            WorldTime = WorldTime.AddHours(Time.deltaTime);
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