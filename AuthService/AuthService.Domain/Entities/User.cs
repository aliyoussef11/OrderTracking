using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty; // By Default Empty string
        public string HashedPassword { get; set; } = string.Empty;

        // Ctor (Add User)
        public User(string email, string hashedPassword)
        {
            Id = Guid.NewGuid();
            Email = email;  
            HashedPassword = hashedPassword;
        }
    }
}
