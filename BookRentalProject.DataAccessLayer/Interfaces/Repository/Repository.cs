using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookRentalProject.Infrastructure.Data;
using BookRentalProject.Infrastructure.Interfaces.IRepository;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace BookRentalProject.Infrastructure.Interfaces.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {

            _context = context;
            _dbSet = context.Set<T>();
        }
        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void deleterange(IEnumerable<T> entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>>? predicate = null, string? includProperties = null)
        {
            throw new NotImplementedException();
        }

        public T getT(System.Linq.Expressions.Expression<Func<T, bool>>? predicate, string? includProperties = null)
        {
            throw new NotImplementedException();
        }
    }
}
