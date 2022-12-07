using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.Models;

namespace UserManagement.Core.Interfaces.Services
{
    public interface IUserService
    {
        List<User> GetUser();
        void AddUser(User user);
        void EditUser(User user);
        void DeleteUser(int Id);
        bool AuthenticateUser(UserLoginModel model);
    }
}
