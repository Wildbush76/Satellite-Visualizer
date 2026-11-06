using System.Collections.Generic;
using UnityEngine;
using One_Sgp4;
using System;
using Unity.XR.CoreUtils;
namespace Assets.Scripts.Satellite
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Satellite : MonoBehaviour
    {
        private const float orbitRange = 10;//unity units - This is how far they will actually be
        public static readonly Dictionary<int, Satellite> satellites = new();
        private Transform focus;//the point all the satellites look at
        private SpriteRenderer sprite;
        private SatelliteType type = SatelliteType.UNKNOWN;

        public int SatNumber
        {
            get => tle.getSatNumber();
        }
        private Tle tle;

        public void Initialize(Tle tle, SatelliteType type)
        {
            this.tle = tle;
            this.type = type;
            satellites.Add(SatNumber, this);
        }

        void Start()
        {
            focus = transform.parent;
            sprite = GetComponent<SpriteRenderer>();
            sprite.color = GetSatelliteColor(type);
        }

        private static Color GetSatelliteColor(SatelliteType type)
        {
            return type switch
            {
                SatelliteType.GNSS => Color.blue,
                _ => Color.gray,
            };
        }


        public void UpdatePosition(EpochTime now, Coordinate observer)
        {
            Sgp4Data data = SatFunctions.getSatPositionAtTime(tle, now, Sgp4.wgsConstant.WGS_84);
            Point3d location = SatFunctions.calcSphericalCoordinate(observer, now, data);

            float azimuth = (float)location.y;
            float elevation = (float)location.z;

            float x = orbitRange * Mathf.Cos(elevation) * Mathf.Sin(azimuth);
            float y = orbitRange * Mathf.Sin(elevation);
            float z = orbitRange * Mathf.Cos(elevation) * Mathf.Cos(azimuth);

            transform.localPosition = new Vector3(x, y, z);

            var to = focus.position - transform.position;
            transform.rotation = Quaternion.LookRotation(to, Vector3.up);
        }

        private void OnDestroy()
        {
            if (tle is not null)
                satellites.Remove(SatNumber);
        }
    }
}