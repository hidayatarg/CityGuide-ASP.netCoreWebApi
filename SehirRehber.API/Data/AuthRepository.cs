using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SehirRehber.API.Models;

namespace SehirRehber.API.Data
{
    public class AuthRepository:IAuthRepository
    {
        private DataContext _context;

        public async Task<User> Register(User user, string password)
        {
            // Salting Password
            // Hasing Password

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac= new System.Security.Cryptography.HMACSHA512())
            {
                // key can be also used
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        // Hashing Method


        public Task<User> Login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserExits(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
