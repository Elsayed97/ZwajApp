using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZwajApp.API.Models;
using System.Security.Cryptography;
using System;
using System.Text;

namespace ZwajApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> isUserExists(string userName)
        {
            return await _context.Users.AnyAsync(u => u.UserName == userName);
        }

        public async Task<User> Login(string userName , string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if(user == null) return null;
            if(!verifyPasswordHash(password,user.PasswordSalt, user.PasswordHash)) return null;
            return user;
        }

        private bool verifyPasswordHash(string password, byte[] saltPassword, byte[] hashPassword)
        {
            using(var hmac = new HMACSHA512(saltPassword)){
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                     if(computedHash[i] != hashPassword[i]){
                         return false;
                     }
                }
                return true;
            }
            
        }

        public async Task<User> Register(User user, string password)
        {
            byte [] saltPassword , hashPassword;
            cretaeHashedPassword(password , out saltPassword , out hashPassword);
            user.PasswordSalt = saltPassword;
            user.PasswordHash = hashPassword;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;

        }

        private void cretaeHashedPassword(string password, out byte[] saltPassword, out byte[] hashPassword)
        {
            using(var hmac = new HMACSHA512()){
                saltPassword = hmac.Key;
                hashPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}