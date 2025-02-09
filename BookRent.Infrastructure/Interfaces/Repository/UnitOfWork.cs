using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BookRent.Application.Interfaces.IRepository;
using BookRent.Domain.Entities;
using BookRent.Domain.ViewModels;
using BookRent.Infrastructure.Data;
using BookRent.Infrastructure.Interfaces.Repository;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;

namespace BookRent.Infrastructure.Interfaces.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public IBookRepo Book { get; private set; }

        public IGenreRepository  Genre{ get; private set; }

        public IUserService UserService { get; private set; }

        public UnitOfWork(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            Book=new BookRepo(context);
            Genre=new GenreRepo(context);
            UserService=new UserService(context, configuration);
        }



        //public IGenericRepo<Book> Books => new GenericRepo<Book>(_context);
        //public IGenericRepo<Genre> Genres => new GenericRepo<Genre>(_context);


        public void Dispose()
        {
            _context.Dispose();
        }

        //public Task<ResponseModel> Register(UserRegisteration user)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ResponseModel> Authenticate(string email, string password)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IReadOnlyList<UserRegisteration>> GetAllAsync(Expression<Func<UserRegisteration, bool>>? predicate, Func<IQueryable<UserRegisteration>, IIncludableQueryable<UserRegisteration, object>> include)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<UserRegisteration?> GetByIdAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Add(UserRegisteration Entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Delete(int Id)
        //{
        //    throw new NotImplementedException();
        //}

        //public void DeleteRange(IEnumerable<UserRegisteration> Entity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
