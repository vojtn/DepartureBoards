using Microsoft.Extensions.Options;
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
        /// <summary>
        /// Adds a favorite board (stop) for a given user if it doesn't already exist.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="stopInfo"></param>
        public abstract void TryAddBoard(string username, StopInfo stopInfo);
        /// <summary>
        /// Edits an existing favorite board (stop) for a given user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="stopInfo"></param>
        public abstract void EditBoard(string username, StopInfo stopInfo);
    }
    public class FileHandler : DataHandler
    {
        private const string path = "./Data/data.json";
        private Users? ReadFile()
        {
            using FileStream openStream = File.OpenRead(path);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<Users>(openStream, options);
            openStream.Close();

        }
        private void Write(Users users)
        {
            string updatedJson = JsonSerializer.Serialize(users, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(path, updatedJson);
        }
        public override bool TryAddUser(string username)
        {
            Users? users = ReadFile();
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
                    Write(users);
                    return true;
                }
            }
            return false;
        }

        public override User? GetUser(string username)
        {
            Users? users = ReadFile();
            if (users is null)
            {
                return null;
            }
            return users!.GetUser(username);
        }

        public override void DeleteBoard(string username, string stopName)
        {
            Users? users = ReadFile();
            if (users is null)
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
            Write(users);
        }

        public override void TryAddBoard(string username, StopInfo favorite)
        {
            Users? users = ReadFile();

            if (users is not null)
            {
                var user = users.GetUser(username);
                if (user != null)
                {
                    // Check for duplicates before adding
                    bool alreadyExists = user.Favorites.Any(f => f.Name == favorite.Name);
                    if (!alreadyExists)
                        user.Favorites.Add(favorite);
                    Write(users);
                }
                else
                {
                    Console.WriteLine("User not found.");
                }
            }
        }
        public override void EditBoard(string username, StopInfo stopInfo)
        {
            Users? users = ReadFile();

            if (users is not null)
            {
                var user = users.GetUser(username);
                if (user != null)
                {
                    var existingBoard = user.Favorites.FirstOrDefault(f => f.Name == stopInfo.Name);
                    if (existingBoard != null)
                    {
                        // Update the existing board's platforms
                        existingBoard.Platforms = stopInfo.Platforms;
                        Write(users);
                    }
                    else
                    {
                        Console.WriteLine("Board not found.");
                    }
                }
                else
                {
                    Console.WriteLine("User not found.");
                }
            }
        }
    }
}
