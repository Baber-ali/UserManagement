using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.Interfaces.Repositories;
using UserManagement.Core.Interfaces.Services;
using UserManagement.Core.Models;

namespace UserManagement.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public void AddUser(User user)
        {
            _repo.AddUser(user);
        }

        public void DeleteUser(int Id)
        {
            _repo.DeleteUser(Id);
        }

        public void EditUser(User user)
        {
            _repo.EditUser(user);
        }

        public List<User> GetUser()
        {
            return _repo.GetUser();
        }

        public bool AuthenticateUser(UserLoginModel model)
        {
            return _repo.AuthenticateUser(model);
        }
    }
}
