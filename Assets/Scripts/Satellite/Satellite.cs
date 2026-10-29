using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Satellite
{

    public class Satellite : MonoBehaviour
    {


        public static readonly List<Satellite> satellites = new();
        public readonly OrbitInformation orbitInformation;



        void Start()
        {
            satellites.Add(this);



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