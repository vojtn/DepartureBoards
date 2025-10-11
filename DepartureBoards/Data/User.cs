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
    }
}
