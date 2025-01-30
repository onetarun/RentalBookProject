using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookRentalProject.Infrastructure.Interfaces.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression <Func<T,bool>>? predicate = null,string? includProperties=null );
        T getT(Expression<Func<T, bool>>? predicate , string? includProperties = null);

        void Add(T entity);
       // void Update(T entity);
        void Delete(T entity);
        void deleterange(IEnumerable<T> entity);

    }
}
