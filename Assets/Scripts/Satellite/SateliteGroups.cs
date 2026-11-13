namespace Assets.Scripts.Satellite
{
    public static class SatelliteGroups//TODO full fill these in
    {
        public static class Navigation
        {
            public static readonly string GPS_OPERATIONAL = "gps-ops";
            public static readonly string GLONASS_OPERATIONAL = "glo-ops";
            public static readonly string GALILEO = "galileo";
            public static readonly string BEIDOU = "beidou";
            public static readonly string SBAS = "sbas";
        }

        public static class Communication
        {
            public static readonly string STARLINK = "starlink";
        }
    }
}