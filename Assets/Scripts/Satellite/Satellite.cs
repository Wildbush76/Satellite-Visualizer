using System.Collections.Generic;
using UnityEngine;
using One_Sgp4;
using System;
using Unity.XR.CoreUtils;
using Assets.Scripts.Sgp4Management;
using One_Sgp4.omm;
namespace Assets.Scripts.Satellite
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Satellite : MonoBehaviour
    {
        private const float orbitRange = 10;//unity units - This is how far they will actually be

        private Transform focus;//the point all the satellites look at
        private SpriteRenderer sprite;
        private Sgp4 propagator;

        private Omm omm;


        public void Initialize(Omm omm)
        {
            this.omm = omm;
            propagator = new(omm, Sgp4.wgsConstant.WGS_84);
        }

        void Start()
        {
            focus = transform.parent;
            sprite = GetComponent<SpriteRenderer>();
            sprite.color = Color.gray;//TODO implement color
        }


        public void UpdatePosition(EpochTime now, Coordinate observer)
        {
            propagator.runSgp4Cal(now, now, 1.0 / 60.0);
            Sgp4Data data = propagator.getResults()[0];
            propagator.clear();
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


    }
}