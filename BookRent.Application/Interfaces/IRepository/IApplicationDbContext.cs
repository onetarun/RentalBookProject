using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookRent.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookRent.Application.Interfaces.IRepository
{
    public interface IApplicationDbContext
    {
        DbSet<Book> Books { get; }
        DbSet<Genre> Genres { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
      


    }
}
