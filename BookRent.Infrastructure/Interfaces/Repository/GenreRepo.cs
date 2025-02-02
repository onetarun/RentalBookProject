using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookRent.Application.Interfaces.IRepository;
using BookRent.Domain.Entities;
using BookRent.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookRent.Infrastructure.Interfaces.Repository
{
    public class GenreRepo : GenericRepo<Genre>, IGenreRepository
    {
        private readonly ApplicationDbContext _context;
        public GenreRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public bool IsExists(int id)
        {
            return _context.Set<Genre>().Any(x => x.GenreID == id);
        }



        public async Task UpdateBookAsync(Genre genre)
        {
            _context.Set<Genre>().Update(genre);
            await (_context as ApplicationDbContext).SaveChangesAsync();
        }
    }
}
