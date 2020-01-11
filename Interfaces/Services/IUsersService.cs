using Models.Authentication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface IUsersService
    {
        Task<User> Authenticate(UserLogin user);
        Task BanUser(User user, string motive, TimeSpan duration);
        Task CreateUser(User user);
        Task DeleteUser(User user);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByToken(string token);
        Task UpdateUser(User user);
    }
}