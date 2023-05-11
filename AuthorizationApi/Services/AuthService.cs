using System.Security.Cryptography;
using System.Threading.Tasks;
using System;
using AuthorizationApi.DBContext;
using AuthorizationApi.Models;
using Microsoft.EntityFrameworkCore;
using AuthorizationApi.Services.Interfaces;

namespace AuthorizationApi.Services
{
    public class AuthService : IAuthService
    {
        private MyDBContext _dbContext;

        public AuthService(MyDBContext context)
        {
            _dbContext = context;
        }

        public async Task<User> GetUser(string username, string password)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(s => s.Username == username);
            if (user != null)
            {

                /* Fetch the stored value */
                string savedPasswordHash = user.PasswordHash;
                /* Extract the bytes */
                byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
                /* Get the salt */
                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);
                /* Compute the hash on the password the user entered */
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
                byte[] hash = pbkdf2.GetBytes(20);
                /* Compare the results */
                for (int i = 0; i < 20; i++)
                {
                    if (hashBytes[i + 16] != hash[i])
                    {
                        return null;
                    }
                }
                return user;
            }
            return null;
        }
        public async Task<string> GetUserType(User user)
        {
            var userType = await _dbContext.UserTypes.FindAsync(user.UserType_ID);
            if (userType != null)
            {
                return userType.UserTypeName;
            }
            else
            {
                return null;
            }
        }
    }
}
