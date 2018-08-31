using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SehirRehber.API.Models;

namespace SehirRehber.API.Data
{
    public interface IAuthRepository
    {
        // Give a user to Register
        Task<User> Register(User user, string password);

        // Login
        Task<User> Login(string userName, string password);

        // Find if User exists
        Task<bool> UserExits(string userName);
    }
}
