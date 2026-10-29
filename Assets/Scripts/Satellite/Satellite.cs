using System.Collections.Generic;
using UnityEngine;
using One_Sgp4;
namespace Assets.Scripts.Satellite
{

    public class Satellite : MonoBehaviour
    {

        public static readonly List<Satellite> satellites = new();
        public readonly OrbitInformation orbitInformation;



        private void Start()
        {
            satellites.Add(this);



        }
        private void OnDestroy()
        {
            satellites.Remove(this);
        }


        private void Update()
        {

        }
    }
}