
using System.Collections.Generic;
using System.IO;
using Assets.Scripts.Celestrak;
using UnityEngine;

namespace Assets.Scripts.Satellite
{

    public sealed record SatelliteTypes
    {
        public enum SatelliteGroup
        {
            GNSS,
            COMMUNICATION,
            NONE

        }
        public static readonly List<SatelliteTypes> ALL_GROUPS = new();

        public string Name { get; }
        public string CelestrakId { get; }
        public Color Color { get; }
        public SatelliteGroup Category { get; }
        public string OmmFile { get => Path.Combine(Application.persistentDataPath, CelestrakId + ".xml"); }
        public string CelestrakQuery { get => Downloader.GetCelestrakUrl("GROUP", CelestrakId, "xml"); }


        private SatelliteTypes(string name, string celestrakId, Color color, SatelliteGroup category)
        {
            Name = name;
            CelestrakId = celestrakId;
            Color = color;
            Category = category;

            ALL_GROUPS.Add(this);
        }

        //GNSS
        public static readonly SatelliteTypes GPS_OPERATIONAL = new("GPS", "gps-ops", Color.blue, SatelliteGroup.GNSS);
        public static readonly SatelliteTypes GLONASS_OPERATIONAL = new("GLONASS", "glo-ops", Color.lightBlue, SatelliteGroup.GNSS);
        public static readonly SatelliteTypes GALILEO = new("GALILEO", "galileo", Color.darkBlue, SatelliteGroup.GNSS);
        public static readonly SatelliteTypes BEIDOU = new("BEIDOU", "beidou", Color.blueViolet, SatelliteGroup.GNSS);
        public static readonly SatelliteTypes SBAS = new("SBAS", "sbas", Color.skyBlue, SatelliteGroup.GNSS);

        //COMMUNICATION
        public static readonly SatelliteTypes STARLINK = new("Starlink", "starlink", Color.yellow, SatelliteGroup.COMMUNICATION);
    }


}