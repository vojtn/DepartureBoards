namespace DepartureBoards.Data
{
    public class Person
    {
        public string Name { get; set; }
        //public List<string> Favourites { get; set; }

        public List<StopInfo> Favorites { get;set;}

        public void DeleteStop(string stopName)
        {
            Favorites.RemoveAll(stop => stop.Name == stopName);
        }
    }
    public class StopInfo
    {
        public string Name { get; set; }
        public List<string> Platforms { get; set; }
    }
    public class People
    {
        public List<Person> users { get; set; }

        public bool Contains(string username)
        {
            return users.Any(user => user.Name == username);
        }
        public Person? GetUser(string username)
        {
            return users.FirstOrDefault(user => user.Name == username);
        }
    }
}
