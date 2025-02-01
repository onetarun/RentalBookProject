using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookRent.Application.Interfaces.IRepository;
using BookRent.Domain.Entities;
using Microsoft.EntityFrameworkCore;



namespace BookRent.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext,IApplicationdbContext
    {

        //    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        //    {

        //    }
        //    public DbSet<Book> Books { get; set; }
        //    public DbSet<Genre> Genres { get; set; }
        //}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Provide implementation for Set<T>
        DbSet<T> IApplicationdbContext.Set<T>()
        {
            return base.Set<T>();
        }

        // Provide implementation for SaveChangesAsync
        //public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    return await base.SaveChangesAsync(cancellationToken);
        //}

        //// Define DbSets for your entities (e.g., Book, User)
        //public DbSet<Book> Books { get; set; }
        //public DbSet<Genre> Genres { get; set; }
    }
}
