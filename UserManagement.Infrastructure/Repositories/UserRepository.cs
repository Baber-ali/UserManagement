using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.Interfaces.Repositories;
using UserManagement.Core.Models;

namespace UserManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        List<User> _users = new()
        {
            new User() { Id = 1, UserName = "baber", Password = "123", Email = "baberali@gmail.com" },
            new User() { Id = 2, UserName = "athar", Password = "abc", Email = "athar@gmail.com" },
            new User() { Id = 3, UserName = "mubeen", Password = "xyz", Email = "mubeen@gmail.com" }
        };

        public void AddUser(User user)
        {
            int Id = _users.Select(u => u.Id).Max();
            user.Id = Id + 1;

            _users.Add(user);
        }

        public void DeleteUser(int Id)
        {
            User? userData = _users.FirstOrDefault(u => u.Id == Id);

            if(userData is not null)
            {
                _users.Remove(userData);
            }
        }

        public void EditUser(User user)
        {
            User? userData = _users.FirstOrDefault(u => u.Id == user.Id);
            if (userData is not null)
            {
                userData.UserName = user.UserName;
                userData.Password = user.Password;
                userData.Email = user.Email;
            }
        }

        public List<User> GetUser()
        {
            return _users;
        }

        public bool AuthenticateUser(UserLoginModel model)
        {
            return _users.Where(u => u.UserName == model.UserName && u.Password == model.Password)
                         .Any();
        }
    }
}
