using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using BookRent.Application.Interfaces.IRepository;
using BookRent.Infrastructure.Data;

namespace BookRent.Infrastructure.Interfaces.Repository
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _dbSet;


        public GenericRepo(ApplicationDbContext context)
        {

            _context = context;
            _dbSet = context.Set<T>();
        }
        public void Add(T Entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<T> Entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>>? predicate = null, string? includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public T GetByID(System.Linq.Expressions.Expression<Func<T, bool>>? predicate, string? includeProperties = null)
        {
            throw new NotImplementedException();
        }
    }
}
