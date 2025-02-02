using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using BookRent.Application.Interfaces.IRepository;
using BookRent.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Query;

namespace BookRent.Infrastructure.Interfaces.Repository
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepo(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            _context.SaveChanges();
        }

        //public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, string? includeProperties = null)
        //{
        //    IQueryable<T> query = _dbSet;

        //    if (predicate != null)
        //    {
        //        query = query.Where(predicate);
        //    }

        //    if (!string.IsNullOrEmpty(includeProperties))
        //    {
        //        foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        //        {
        //            query = query.Include(includeProperty);
        //        }
        //    }

        //    return await query.ToListAsync();
        //}

        public async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (include != null)
            {
                query = include(query);
            }

            return await query.ToListAsync();
        }


        //public async Task<T> GetByIDAsync(Expression<Func<T, bool>>? predicate, string? includeProperties = null)
        //{
        //    IQueryable<T> query = _dbSet;

        //    if (predicate != null)
        //    {
        //        query = query.Where(predicate);
        //    }

        //    if (!string.IsNullOrEmpty(includeProperties))
        //    {
        //        foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        //        {
        //            query = query.Include(includeProperty);
        //        }
        //    }

        //    return await query.FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Entity not found");
        //}
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id) ?? throw new KeyNotFoundException($"Entity with id {id} not found.");
        }
    }
}
