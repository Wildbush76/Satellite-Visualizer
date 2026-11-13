using System;
using One_Sgp4;

namespace Assets.Scripts.Sgp4Management
{
    [Serializable]
    public readonly struct Sgp4Management
    {
        public readonly string OBJECT_NAME;
        public readonly string OBJECT_ID;
        public readonly DateTime EPOCH;
        public readonly double MEAN_MOTION;
        public readonly double ECCENTRICITY;
        public readonly double INCLINATION;
        public readonly double RA_OF_ASC_NODE;
        public readonly double ARG_OF_PERICENTER;
        public readonly double MEAN_ANOMALY;
        public readonly double EPHEMERIS_TYPE;
        public readonly string CLASSIFICATION_TYPE;
        public readonly short NORAD_CAT_ID;
        public readonly short ELEMENT_SET_NO;
        public readonly int REV_AT_EPOCH;
        public readonly double BSTAR;
        public readonly double MEAN_MOTION_DOT;
        public readonly double MEAN_MOTION_DDOT;


        public Tle ToTle()
        {
            return null;//TODO implement this
        }
    }
}