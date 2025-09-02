using System.Text.Json;

namespace DepartureBoards.Data
{
    public abstract class DataHandler
    {
        /// <summary>
        /// Adds a new user to the data storage. Returns false if the username already exists
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public abstract bool TryAddUser(string username);
        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public abstract User? GetUser(string username);
        /// <summary>
        /// Deletes a favorite board (stop) for a given user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="stopName"></param>
        public abstract void DeleteBoard(string username, string stopName);
        public abstract void EditBoard(StopInfo stopInfo);
    }
    public class FileHandler : DataHandler
    {
        private const string path = "./Data/data.json";
        public override bool TryAddUser(string username)
        {
            using FileStream openStream = File.OpenRead(path);
            var users = JsonSerializer.Deserialize<Users>(openStream);
            if(users is not null)
            {
                if (users!.Contains(username))
                {
                    return false;
                }
                else
                {
                    var newUser = new User(username);
                    users.users.Add(newUser);
                    openStream.Close();
                    string jsonString = JsonSerializer.Serialize(users);
                    File.WriteAllText(path, jsonString);
                    return true;
                }
            }
            return false;
        }
        public override User? GetUser(string username)
        {
            using FileStream openStream = File.OpenRead(path);
            var users = JsonSerializer.Deserialize<Users>(openStream);
            openStream.Close();
            if(users is null)
            {
                return null;
            }
            return users!.GetUser(username);
        }

        public override void DeleteBoard(string username, string stopName)
        {
            string json = File.ReadAllText(path);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var users = JsonSerializer.Deserialize<Users>(json, options);
            if(users is null)
            {
                return;
            }
            var user = users.GetUser(username);
            if (user != null)
            {
                if (user.Favorites != null)
                {
                    user.RemoveStop(stopName);
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

        public override void EditBoard(StopInfo stopInfo)
        {
            throw new NotImplementedException();
        }
    }
}
