using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DepartureBoards.Data
{
    public class StopGroup
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("isTrain")]
        public bool IsTrain { get; set; }
        public override string ToString()
        {
            if(IsTrain)
                return $"{Name} (vlak)";
            else return Name ;
        }
    }

    public class StopData
    {
        [JsonPropertyName("stopGroups")]
        public List<StopGroup> StopGroups { get; set; }
    }

    public class ApiHandler
    {
        public HttpClient httpClient = new();
        private string stopname { get; set; }
        private int limit { get; set; }
        private string baseurl = $"https://api.golemio.cz/v2/pid/departureboards";
        private const string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MzAyMSwiaWF0IjoxNzI3ODYyODMwLCJleHAiOjExNzI3ODYyODMwLCJpc3MiOiJnb2xlbWlvIiwianRpIjoiYWZmMTNlOTYtY2ZjZi00MThkLThkMTQtOGExODc1M2ZkNGI0In0.kEPOLWFHlyAtqD7pxtGIYj3BaEr1BWgnBkJfejZTu5o";
        public async Task<List<Departure>> GetDepartureBoard(string stopname, int limit = 8)
        {
            //?names={stop}&limit={limit}
            try
            {
                string url = baseurl + $"?names={stopname}&limit={limit}";
                // Fetch the updated data
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                // Add the X-Access-Token header
                request.Headers.Add("X-Access-Token", accessToken);

                // Send the request and get the response
                var response = await httpClient.SendAsync(request);
                Console.WriteLine("refresh");
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content and deserialize it
                    var data = await response.Content.ReadFromJsonAsync<DepartureBoardResponse>();
                    return data.Departures;
                    //await InvokeAsync(StateHasChanged);
                    var a = data.Departures.GroupBy(departure => departure.Stop.PlatformCode);
                }
                else
                {
                    string errorMessage = $"Error: Unable to fetch weather data. Status code: {response.StatusCode}";
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching departure data: {ex.Message}");
                return null;
            }
        }
        public async Task<List<string>> GetStopNamesAsync()
        {
            var url = "https://data.pid.cz/stops/xml/StopsByName.xml";
            using var client = new HttpClient();
            string xmlContent = await client.GetStringAsync(url);

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlContent);

            var navigator = xmlDoc.CreateNavigator();
            var xpath = "//group/@name";
            var nodes = navigator.Select(xpath);

            var groupNames = new List<string>();
            int i = 0;
            while (nodes.MoveNext())
            {
                groupNames.Add(nodes.Current.Value);
                i++;
                if (i == 25)
                {
                    break;
                }
            }
            return groupNames;

        }
        public async Task<Stops?> GetStopsAsync()
        {
            //string stopsUrl = "https://data.pid.cz/stops/json/stops.json";
            //var request = new HttpRequestMessage(HttpMethod.Get, stopsUrl);

            //// Add the X-Access-Token header
            //request.Headers.Add("X-Access-Token", accessToken);

            //// Send the request and get the response
            //var response = await httpClient.SendAsync(request);
            //return "";

            var url = "https://data.pid.cz/stops/xml/StopsByName.xml";
            using var client = new HttpClient();
            string xmlContent = await client.GetStringAsync(url);

            var serializer = new XmlSerializer(typeof(Stops));
            using var reader = new StringReader(xmlContent);
            var stopsData = (Stops)serializer.Deserialize(reader);

            Console.WriteLine($"Data Format Version: {stopsData.DataFormatVersion}");
            Console.WriteLine($"Generated At: {stopsData.GeneratedAt}");
            Console.WriteLine($"Number of groups: {stopsData.Groups?.Count}");

            return stopsData;

            //foreach (var group in stopsData.Groups)
            //{
            //    Console.WriteLine($"\nGroup: {group.Name}, Stops: {group.Stops?.Count}");
            //    foreach (var stop in group.Stops)
            //    {
            //        Console.WriteLine($"  Stop: {stop.Id}, AltName: {stop.AltIdosName}, Zone: {stop.Zone}");
            //        if (stop.Lines != null)
            //        {
            //            foreach (var line in stop.Lines)
            //            {
            //                Console.WriteLine($"    Line: {line.Name}, Type: {line.Type}, Dir: {line.Direction}");
            //            }
            //        }
            //    }
            //    break; // comment this to process all groups
            //}
        }

        public async Task<List<StopGroup>> GetStopsAsyncJson()
        {
            var url = "https://data.pid.cz/stops/json/stops.json";
            using var client = new HttpClient();
            var json = await client.GetStringAsync(url);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var stopData = JsonSerializer.Deserialize<StopData>(json, options);

            //Console.WriteLine("Stop Group Names:");
            //foreach (var group in stopData.StopGroups)
            //{
            //    Console.WriteLine($"- {group.Name}");
            //}
            Console.WriteLine("Loaded");
            return stopData.StopGroups;
        }
    }
}
