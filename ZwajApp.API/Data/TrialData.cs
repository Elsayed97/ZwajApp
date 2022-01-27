using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ZwajApp.API.Data;
using ZwajApp.API.Models;

namespace ZwajApp.Api.Data
{
    public class TrialData
    {
        private readonly DataContext _context;

        public TrialData(DataContext context)
        {
            _context = context;
        }

        public void TrialUsers()
        {
            var usersData = File.ReadAllText("Data/UsersTrialData.json");
            var users = JsonConvert.DeserializeObject<List<User>>(usersData);
            foreach (var user in users)
            {
                byte[] saltPassword, hashPassword;
                CreatePasswordHash("password", out saltPassword, out hashPassword);
                user.PasswordSalt = saltPassword;
                user.PasswordHash = hashPassword;
                user.UserName = user.UserName.ToLower();
                _context.Add(user);
            }
            _context.SaveChanges();
        }

        private void CreatePasswordHash(string password,out byte [] saltPassword, out byte [] hashPassword)
        {
            using(var hmac = new HMACSHA512())
            {
                saltPassword = hmac.Key;
                hashPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); 
            }
        }
    }
}
