using System;
using UnityEngine;

namespace Assets.Scripts.Satellite
{
    [Serializable]
    public struct SatelliteGroupConfig
    {
        public readonly Color color;
        public readonly string name;
        public readonly DateTime lastUpdate;
    }
}