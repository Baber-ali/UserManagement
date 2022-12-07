using UserManagement.Core.Models;

namespace UserManagement.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        List<User> GetUser();
        void AddUser(User user);
        void EditUser(User user);
        void DeleteUser(int Id);
        bool AuthenticateUser(UserLoginModel model);
    }
}
