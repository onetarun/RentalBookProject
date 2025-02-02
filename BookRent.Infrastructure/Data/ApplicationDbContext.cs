using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookRent.Application.Interfaces.IRepository;
using BookRent.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;



namespace BookRent.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext,IApplicationdbContext
    {


        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        //// Provide implementation for Set<T>
        //DbSet<T> IApplicationdbContext.Set<T>()
        //{
        //    return base.Set<T>();
        //}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<T> Set<T>() where T : class => base.Set<T>();

        //public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    return await base.SaveChangesAsync(cancellationToken);
        //}
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Automatically discover all entities in the assembly containing "Genre"
        //    modelBuilder.ApplyConfigurationsFromAssembly(typeof(En).Assembly);

        //    base.OnModelCreating(modelBuilder);
        //}

        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
