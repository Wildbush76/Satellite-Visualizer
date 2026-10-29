using System.Collections.Generic;
using UnityEngine;
using One_Sgp4;
using System;
using Unity.XR.CoreUtils;
namespace Assets.Scripts.Satellite
{

    public class Satellite : MonoBehaviour
    {
        private const float orbitRange = 5;//unity units - This is how far they will actually be
        public static readonly Dictionary<int, Satellite> satellites = new();

        public bool NeedsUpdate { get; private set; }
        public int SatNumber
        {
            get => tle.getSatNumber();
        }
        private Tle tle;

        public void Initialize(Tle tle)
        {
            this.tle = tle;
            satellites.Add(SatNumber, this);
            NeedsUpdate = true;
        }

        public void UpdatePosition(EpochTime now, Coordinate observer)
        {
            Sgp4Data data = SatFunctions.getSatPositionAtTime(tle, now, Sgp4.wgsConstant.WGS_84);
            Point3d location = SatFunctions.calcSphericalCoordinate(observer, now, data);

            float azimuth = (float)location.y;
            float elevation = (float)location.z;

            Debug.Log("Azimuth : " + azimuth + " elevation : " + elevation);

            float x = orbitRange * Mathf.Cos(elevation) * Mathf.Sin(azimuth);
            float y = orbitRange * Mathf.Sin(elevation);
            float z = orbitRange * Mathf.Cos(elevation) * Mathf.Cos(azimuth);

            transform.localPosition = new Vector3(x, y, z);
            NeedsUpdate = false;

            //TODO add calculation for when the next position update should be
        }

        private void OnDestroy()
        {
            if (tle is not null)
                satellites.Remove(SatNumber);
        }


        private void Update()
        {

        }
    }
}