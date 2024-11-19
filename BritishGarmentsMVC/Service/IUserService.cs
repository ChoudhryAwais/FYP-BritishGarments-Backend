using BritishGarmentsMVC.Models;

namespace BritishGarmentsMVC.Service
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
    }
    public class UserService(ApplicationDbContext context) : IUserService
    {
        private readonly ApplicationDbContext _context = context;
        //Sale services
        public IEnumerable<User> GetAllUsers()
        {
            return [.. _context.Users];
        }
    }
}
