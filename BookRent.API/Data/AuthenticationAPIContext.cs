using BookRent.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookRent.API.Data
{
    public class AuthenticationAPIContext : IdentityDbContext<ApplicationUser>
    {
        //public AuthenticationAPIContext()
        //{

        //}
        public AuthenticationAPIContext(DbContextOptions<AuthenticationAPIContext> options) : base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
