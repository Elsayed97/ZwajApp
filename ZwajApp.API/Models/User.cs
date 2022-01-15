using System;

namespace ZwajApp.API.Models
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}