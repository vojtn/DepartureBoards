using System.Text.Json.Serialization;

namespace DepartureBoards.Data
{

    public class DepartureBoardResponse
    {
        [JsonPropertyName("stops")]
        public List<Stop> Stops { get; set; }

        [JsonPropertyName("departures")]
        public List<Departure> Departures { get; set; }

        [JsonPropertyName("infotexts")]
        public List<Infotext> Infotexts { get; set; }
    }

    public class Stop
    {
        [JsonPropertyName("location_type")]
        public int LocationType { get; set; }

        [JsonPropertyName("parent_station")]
        public string? ParentStation { get; set; }

        [JsonPropertyName("platform_code")]
        public string PlatformCode { get; set; }

        [JsonPropertyName("stop_id")]
        public string StopId { get; set; }

        [JsonPropertyName("stop_lat")]
        public double StopLat { get; set; }

        [JsonPropertyName("stop_lon")]
        public double StopLon { get; set; }

        [JsonPropertyName("stop_name")]
        public string StopName { get; set; }

        [JsonPropertyName("wheelchair_boarding")]
        public int WheelchairBoarding { get; set; }

        [JsonPropertyName("zone_id")]
        public string ZoneId { get; set; }

        [JsonPropertyName("level_id")]
        public string? LevelId { get; set; }

        [JsonPropertyName("asw_id")]
        public AswId AswId { get; set; }
    }

    public class AswId
    {
        [JsonPropertyName("node")]
        public int Node { get; set; }

        [JsonPropertyName("stop")]
        public int Stop { get; set; }
    }

    public class Departure
    {
        [JsonPropertyName("arrival_timestamp")]
        public Timestamp ArrivalTimestamp { get; set; }

        [JsonPropertyName("delay")]
        public Delay Delay { get; set; }

        [JsonPropertyName("departure_timestamp")]
        public Timestamp DepartureTimestamp { get; set; }

        [JsonPropertyName("last_stop")]
        public LastStop LastStop { get; set; }

        [JsonPropertyName("route")]
        public Route Route { get; set; }

        [JsonPropertyName("stop")]
        public StopDetails Stop { get; set; }

        [JsonPropertyName("trip")]
        public Trip Trip { get; set; }
    }

    public class Timestamp
    {
        [JsonPropertyName("predicted")]
        public DateTime? Predicted { get; set; }

        [JsonPropertyName("scheduled")]
        public DateTime? Scheduled { get; set; }

        [JsonPropertyName("minutes")]
        public string? Minutes { get; set; }
    }

    public class Delay
    {
        [JsonPropertyName("is_available")]
        public bool IsAvailable { get; set; }

        [JsonPropertyName("minutes")]
        public int? Minutes { get; set; }

        [JsonPropertyName("seconds")]
        public int? Seconds { get; set; }
    }

    public class LastStop
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public class Route
    {
        [JsonPropertyName("short_name")]
        public string ShortName { get; set; }

        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("is_night")]
        public bool IsNight { get; set; }

        [JsonPropertyName("is_regional")]
        public bool IsRegional { get; set; }

        [JsonPropertyName("is_substitute_transport")]
        public bool IsSubstituteTransport { get; set; }
    }

    public class StopDetails
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("platform_code")]
        public string PlatformCode { get; set; }
    }

    public class Trip
    {
        [JsonPropertyName("direction")]
        public string? Direction { get; set; }

        [JsonPropertyName("headsign")]
        public string Headsign { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("is_at_stop")]
        public bool IsAtStop { get; set; }

        [JsonPropertyName("is_canceled")]
        public bool IsCanceled { get; set; }

        [JsonPropertyName("is_wheelchair_accessible")]
        public bool IsWheelchairAccessible { get; set; }

        [JsonPropertyName("is_air_conditioned")]
        public bool? IsAirConditioned { get; set; }

        [JsonPropertyName("short_name")]
        public string? ShortName { get; set; }
    }

    public class Infotext
    {
        // Assuming infotexts might contain strings or any other information.
        // If you know the exact structure, update accordingly.
    }
}
