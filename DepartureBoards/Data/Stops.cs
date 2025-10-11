namespace DepartureBoards.Data
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;
    /// <summary>
    /// Model for stops names and their lines.
    /// </summary>
    [XmlRoot("stops")]
    public class Stops
    {
        [XmlElement("group")]
        public List<Group> Groups { get; set; }

        [XmlAttribute("generatedAt")]
        public DateTime GeneratedAt { get; set; }

        [XmlAttribute("dataFormatVersion")]
        public string DataFormatVersion { get; set; }
    }

    public class Group
    {
        [XmlElement("stop")]
        public List<Stop2> Stops { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("districtCode")]
        public string DistrictCode { get; set; }

        [XmlAttribute("isTrain")]
        public bool IsTrain { get; set; }

        [XmlAttribute("idosCategory")]
        public int IdosCategory { get; set; }

        [XmlAttribute("idosName")]
        public string IdosName { get; set; }

        [XmlAttribute("fullName")]
        public string FullName { get; set; }

        [XmlAttribute("uniqueName")]
        public string UniqueName { get; set; }

        [XmlAttribute("node")]
        public int Node { get; set; }

        [XmlAttribute("cis")]
        public int Cis { get; set; }

        [XmlAttribute("avgLat")]
        public float AvgLat { get; set; }

        [XmlAttribute("avgLon")]
        public float AvgLon { get; set; }

        [XmlAttribute("avgJtskX")]
        public float AvgJtskX { get; set; }

        [XmlAttribute("avgJtskY")]
        public float AvgJtskY { get; set; }

        [XmlAttribute("municipality")]
        public string Municipality { get; set; }

        public override string ToString()
        {
            if (IsTrain)
                return $"{Name} (vlak)";
            else return Name;
        }
    }

    public class Stop2
    {
        [XmlElement("line")]
        public List<Line> Lines { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("platform")]
        public string Platform { get; set; }

        [XmlAttribute("altIdosName")]
        public string AltIdosName { get; set; }

        [XmlAttribute("lat")]
        public float Lat { get; set; }

        [XmlAttribute("lon")]
        public float Lon { get; set; }

        [XmlAttribute("jtskX")]
        public float JtskX { get; set; }

        [XmlAttribute("jtskY")]
        public float JtskY { get; set; }

        [XmlAttribute("zone")]
        public string Zone { get; set; }

        [XmlAttribute("gtfsIds")]
        public string GtfsIds { get; set; }

        [XmlAttribute("wheelchairAccess")]
        public WheelchairAccessType WheelchairAccess { get; set; }

        [XmlAttribute("isMetro")]
        public bool IsMetro { get; set; }
    }


    public class Line
    {
        [XmlText]
        public string Value { get; set; }

        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("type")]
        public TrafficType Type { get; set; }

        [XmlAttribute("isNight")]
        public bool IsNight { get; set; }

        [XmlAttribute("direction")]
        public string Direction { get; set; }

        [XmlAttribute("direction2")]
        public string Direction2 { get; set; }
    }

    public enum WheelchairAccessType
    {
        unknown,
        notPossible,
        possible
    }

    public enum TrafficType
    {
        metro,
        tram,
        train,
        funicular,
        bus,
        ferry,
        trolleybus
    }
}
