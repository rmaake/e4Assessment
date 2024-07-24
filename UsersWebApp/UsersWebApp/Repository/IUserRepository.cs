using UsersWebApp.Models;

namespace UsersWebApp.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserById(int id);
        User Save(User user);
        void Delete(int id);
    }
}
