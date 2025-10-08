using Microsoft.EntityFrameworkCore;

namespace DepartureBoards.Data
{
    public interface IUserService
    {
        public Task<User?> GetUserAsync(string name);
        /// <summary>
        /// Adds user to the database
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(User user);
        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task UpdateUser(User user);
        /// <summary>
        /// Deletes user from the database
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task DeleteUserAsync(string name);
    }
    public class UserService : IUserService
    {
        AppDbContext dbContext;
        public UserService(IConfiguration configuration)
        {
            dbContext = new AppDbContext(configuration);
        }
        /// <summary>
        /// Checks if any user with the given name already exists
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool Contains(string username)
        {
            return dbContext.Users.Any(u => u.Name == username);
        }

        public void AddUser(User user)
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }

        public async Task DeleteUserAsync(string name)
        {
            var user = await dbContext.Users.FindAsync(name);
            if (user != null)
            {
                dbContext.Users.Remove(user);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<User?> GetUserAsync(string name)
        {
            return await dbContext.Users.Include("Boards").SingleOrDefaultAsync(user => user.Name == name);
        }

        public async Task UpdateUser(User user)
        {
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();

        }

        public async Task<Board?> GetBoardAsync(int? id)
        {
            return await dbContext.Boards.FindAsync(id);
        }

        public void AddBoard(Board board)
        {
            dbContext.Boards.Add(board);
            dbContext.SaveChanges();
        }

        public async Task UpdateBoardAsync(Board board)
        {
            dbContext.Boards.Update(board);
            await dbContext.SaveChangesAsync();
        }
    }
}
