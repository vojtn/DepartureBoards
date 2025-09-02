namespace DepartureBoards.Data
{
    /// <summary>
    /// Represents a user with a name and a list of favorite stops.
    /// </summary>
    public class User
    {
        public string Name { get; set; } = String.Empty;
        public List<StopInfo> Favorites { get; set; } = new();
        public User(string username) 
        {
            Name = username;
        }
        /// <summary>
        /// Remomes a stop from the favorites list by its name.
        /// </summary>
        /// <param name="stopName"></param>
        public void RemoveStop(string stopName)
        {
            Favorites.RemoveAll(stop => stop.Name == stopName);
        }
    }
    /// <summary>
    /// Represents information about a stop, including its name and associated platforms.
    /// </summary>
    public class StopInfo
    {
        public string Name { get; set; } = String.Empty;
        public List<string> Platforms { get; set; } = new();
    }
    public class Users
    {
        public List<User> users { get; set; } = new();
        /// <summary>
        /// Checks if a user with the given username exists in the users list.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool Contains(string username)
        {
            return users.Any(user => user.Name == username);
        }
        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User? GetUser(string username)
        {
            return users.FirstOrDefault(user => user.Name == username);
        }
    }
}
