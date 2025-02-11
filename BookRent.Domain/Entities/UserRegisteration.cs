using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRent.Domain.Entities
{
    public class UserRegisteration
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } // Store hashed password
        public string Role { get; set; } // Role-based access
        public List<string> Permissions { get; set; } = new();

    }
}
