using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BookRent.Application.Interfaces.IRepository;
using BookRent.Domain.Entities;
using BookRent.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookRent.Infrastructure.Interfaces.Repository
{
    public class BookRepo : GenericRepo<Book>, IBookRepo
    {
        private readonly ApplicationDbContext _context;

        public BookRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetALLBooksWithGenre()
        { 
            return await _context.Set<Book>().Include(x=>x.Genre).ToListAsync(); 
        }
        public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(string author)
        {
            return await _context.Set<Book>().Where(b => b.Author == author).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByGenreAsync(int genreId)
        {
            return await _context.Set<Book>().Where(b => b.GenreId == genreId).ToListAsync();
        }

        public async Task<Book> GetByIDWithGenre(int bookid)
        {
            return await _context.Set<Book>().Include(x => x.Genre).FirstOrDefaultAsync(x => x.BookId == bookid);
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Set<Book>().Update(book);
            await (_context as ApplicationDbContext).SaveChangesAsync();
        }
    }
}
