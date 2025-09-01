using System.Text.Json;

namespace DepartureBoards.Data
{
    public static class FileHandler
    {
        const string path = "./Data/data.json";

        public static void CreateFile(string path)
        {

        }

        public static bool AddUser(string username)
        {
            using FileStream openStream = File.OpenRead(path);
            var people = JsonSerializer.Deserialize<People>(openStream);
            // TODO: people fix null
            if (people.Contains(username))
            {
                // show sth
                return false;
            }
            else
            {
                var newPerson = new Person();
                newPerson.Name = username;
                people.users.Add(newPerson);
                string jsonString = JsonSerializer.Serialize(people);
                openStream.Close();
                File.WriteAllText(path, jsonString);
                return true;
            }
        }
        public static Person? getUser(string username)
        {
            using FileStream openStream = File.OpenRead(path);
            var people = JsonSerializer.Deserialize<People>(openStream);
            openStream.Close();
            return people.GetUser(username);
        }

        public static void DeleteBoard(string username, string stopName)
        {
            string json = File.ReadAllText(path);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var users = JsonSerializer.Deserialize<People>(json, options);

            var user = users.GetUser(username);
            if (user != null)
            {
                if (user.Favorites != null)
                {
                    user.DeleteStop(stopName);
                }
            }
            else
            {
                Console.WriteLine("User not found.");
            }
            string updatedJson = JsonSerializer.Serialize(users, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(path, updatedJson);
        }
    }
}
