using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace BookRent.Application.Interfaces.IRepository
{
    public interface IGenericRepo<T> where T : class
    {
        //Task<IEnumerable<T>> GetAllAsync(Expression <Func<T,bool>>? predicate = null,string? includeProperties=null );
        Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include);
        //Task<T> GetByIDAsync(Expression<Func<T, bool>>? predicate , string? includeProperties = null);
        Task<T?> GetByIdAsync(int id);
        void Add(T Entity); 
        void Delete(int Id);
        void DeleteRange(IEnumerable<T> Entity);

    }
}
