using UnityEngine;
using Assets.Scripts.Sgp4Management;
using Zeptomoby.OrbitTools;
namespace Assets.Scripts.Satellite
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Satellite : MonoBehaviour
    {
        private const float ORBIT_RANGE = 10;//unity units - This is how far they will actually be

        private Transform focus;//the point all the satellites look at
        private SpriteRenderer sprite;
        private Orbit orbit;


        public void Initialize(Omm omm)
        {
            orbit = new(omm);
        }

        void Start()
        {
            focus = transform.parent;
            sprite = GetComponent<SpriteRenderer>();
            sprite.color = Color.orange;//TODO implement color
        }

        public void UpdatePosition(Julian time, EciTime viewerLocation)
        {
            double mpe = orbit.Epoch.Diff(time).TotalMinutes;
            var positionECI = orbit.PositionEci(mpe);
            Vector eci = positionECI.Position;
            Vector viewer = viewerLocation.Position;

            Vector3 toSatellite = new((float)(eci.X - viewer.X), (float)(eci.Y - viewer.Y), (float)(eci.Z - viewer.Z));
            toSatellite = toSatellite.normalized * ORBIT_RANGE;

            transform.position = toSatellite;

            var toFocus = focus.position - transform.position;
            transform.rotation = Quaternion.LookRotation(toFocus, Vector3.up);
        }


    }
}