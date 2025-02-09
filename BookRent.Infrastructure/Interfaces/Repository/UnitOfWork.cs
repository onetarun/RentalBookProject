using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookRent.Application.Interfaces.IRepository;
using BookRent.Domain.Entities;
using BookRent.Infrastructure.Data;
using BookRent.Infrastructure.Interfaces.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BookRent.Infrastructure.Interfaces.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IBookRepo Book { get; private set; }

        public IGenreRepository  Genre{ get; private set; }
        public IUtilityRepo utility { get; private set; }
          
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Book=new BookRepo(context);
            Genre=new GenreRepo(context);
        }



        //public IGenericRepo<Book> Books => new GenericRepo<Book>(_context);
        //public IGenericRepo<Genre> Genres => new GenericRepo<Genre>(_context);


        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
