using DepartureBoards.Data;
using System.ComponentModel.DataAnnotations;

namespace DepartureBoards.Data
{
    /// <summary>
    /// Represents a user with a name and a list of favorite stops.
    /// </summary>
    public class User
    {
        [Key]
        public string Name { get; set; } = String.Empty;
        public List<Board>? Boards { get; set; }
        public User(string Name) 
        {
            this.Name = Name;
        }
        /// <summary>
        /// Remomes a stop from the favorites list by its name.
        /// </summary>
        /// <param name="stopName"></param>
        public void RemoveStop(string stopName)
        {
            //Favorites.RemoveAll(stop => stop.Name == stopName);
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
        /// Checks if a user with the given username exists in the users list (case-insensitive).  
        /// </summary>  
        /// <param name="username"></param>  
        /// <returns></returns>  
        public bool Contains(string username)
        {
            return users.Any(user => string.Equals(user.Name, username, StringComparison.OrdinalIgnoreCase));
        }
        /// <summary>
        /// Retrieves a user by their username (case-insensitive).
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User? GetUser(string username)
        {
            return users.FirstOrDefault(user => string.Equals(user.Name, username, StringComparison.OrdinalIgnoreCase));
        }
    }
}
