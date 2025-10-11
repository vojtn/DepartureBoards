using System.Text.Json.Serialization;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;
using DepartureBoards.Data;

namespace DepartureBoards.Services
{
    public class ApiSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
    }

    public class StopGroup
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("isTrain")]
        public bool IsTrain { get; set; }
        public override string ToString()
        {
            if (IsTrain)
                return $"{Name} (vlak)";
            else return Name;
        }
    }

    public class StopData
    {
        [JsonPropertyName("stopGroups")]
        public List<StopGroup> StopGroups { get; set; }
    }

    public class ApiService
    {
        public HttpClient httpClient = new();
        private string? BoardsUrl { get; set; } = string.Empty;
        private string? StopsUrl { get; set; } = string.Empty;
        private string? AccessToken { get; set; } = string.Empty;

        public ApiService(IConfiguration configuration)
        {
            BoardsUrl = configuration["ApiSettings:BoardsUrl"];
            StopsUrl = configuration["ApiSettings:StopsUrl"];
            AccessToken = configuration["ApiSettings:AccessToken"];
        }
        /// <summary>
        /// Get the departure board for a given stop name.
        /// </summary>
        /// <param name="stopname"></param>
        /// <param name="limit">Stops limit per query</param>
        /// <returns></returns>
        public async Task<List<Departure>?> GetDepartureBoard(string stopname, int limit = 8)
        {
            try
            {
                string url = BoardsUrl + $"?names={stopname}&limit={limit}";

                // Fetch the updated data
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                // Add the X-Access-Token header
                request.Headers.Add("X-Access-Token", AccessToken);

                // Send the request and get the response
                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content and deserialize it
                    var data = await response.Content.ReadFromJsonAsync<DepartureBoardResponse>();
                    if (data is null)
                        return null;
                    return data.Departures;
                }
                else
                {
                    string errorMessage = $"Error: Unable to fetch weather data. Status code: {response.StatusCode}";
                    Console.WriteLine(errorMessage);
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching departure data: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Get all stops from the XML API.
        /// </summary>
        /// <returns></returns>
        public async Task<Stops?> GetStopsAsync()
        {
            // Send the request and get the response
            string xmlContent = await httpClient.GetStringAsync(StopsUrl);

            var serializer = new XmlSerializer(typeof(Stops));
            using var reader = new StringReader(xmlContent);
            var stopsData = serializer.Deserialize(reader) as Stops;

            return stopsData;
        }
    }
}
