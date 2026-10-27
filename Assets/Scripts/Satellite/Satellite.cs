using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Satellite
{

    [RequireComponent(typeof(Transform))]
    public class Satellite : MonoBehaviour
    {

        public static readonly List<Satellite> satellites = new();
        public readonly OrbitInformation orbitInformation;
        private Transform transform;


        void Start()
        {
            satellites.Add(this);

            transform = GetComponent<Transform>();

        }
        void OnDestroy()
        {
            satellites.Remove(this);
        }


        void Update()
        {

        }
    }
}