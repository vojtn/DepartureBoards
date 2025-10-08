namespace DepartureBoards.Data
{
    public class Board
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public int Order { get; set; }
        public List<string> Platforms { get; set; } = new();
        public string UserName { get; set; } // Foreign key
        public User User { get; set; } = null!; // Reference navigation
    }
}
