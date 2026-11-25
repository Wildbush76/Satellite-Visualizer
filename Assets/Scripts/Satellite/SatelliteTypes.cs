
using System.Collections.Generic;
using System.IO;
using Assets.Scripts.Celestrak;
using UnityEngine;

namespace Assets.Scripts.Satellite
{

    public sealed record SatelliteGroupConfig
    {
        public static readonly Dictionary<SatelliteCategory, List<SatelliteGroupConfig>> ALL_GROUPS = new();

        public string Name { get; }
        public string CelestrakId { get; }
        public Color Color { get; }
        public SatelliteCategory Category { get; }
        public string OmmFile { get => Path.Combine(Application.persistentDataPath, CelestrakId + ".xml"); }
        public string CelestrakQuery { get => Downloader.GetCelestrakUrl("GROUP", CelestrakId, "xml"); }


        private SatelliteGroupConfig(string name, string celestrakId, Color color, SatelliteCategory category)
        {
            Name = name;
            CelestrakId = celestrakId;
            Color = color;
            Category = category;

            if (ALL_GROUPS.TryGetValue(Category, out var groupList))
            {
                groupList.Add(this);
            }
            else
            {
                groupList = new()
                {
                    this
                };
                ALL_GROUPS.Add(Category, groupList);
            }
        }

        //GNSS
        public static readonly SatelliteGroupConfig GPS_OPERATIONAL = new("GPS", "gps-ops", Color.blue, SatelliteCategory.GNSS);
        public static readonly SatelliteGroupConfig GLONASS_OPERATIONAL = new("GLONASS", "glo-ops", Color.lightBlue, SatelliteCategory.GNSS);
        public static readonly SatelliteGroupConfig GALILEO = new("GALILEO", "galileo", Color.darkBlue, SatelliteCategory.GNSS);
        public static readonly SatelliteGroupConfig BEIDOU = new("BEIDOU", "beidou", Color.blueViolet, SatelliteCategory.GNSS);
        public static readonly SatelliteGroupConfig SBAS = new("SBAS", "sbas", Color.skyBlue, SatelliteCategory.GNSS);

        //COMMUNICATION
        public static readonly SatelliteGroupConfig STARLINK = new("Starlink", "starlink", Color.yellow, SatelliteCategory.COMMUNICATION);
    }


}