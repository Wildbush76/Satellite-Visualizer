using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.XR.CoreUtils;
using Assets.Scripts.Sgp4Management;
namespace Assets.Scripts.Satellite
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Satellite : MonoBehaviour
    {
        private const float orbitRange = 10;//unity units - This is how far they will actually be

        private Transform focus;//the point all the satellites look at
        private SpriteRenderer sprite;



        public void Initialize()
        {

        }

        void Start()
        {
            focus = transform.parent;
            sprite = GetComponent<SpriteRenderer>();
            sprite.color = Color.gray;//TODO implement color
        }


        public void UpdatePosition(double now)
        {

        }


    }
}