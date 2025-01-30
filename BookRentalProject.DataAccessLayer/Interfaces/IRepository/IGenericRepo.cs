using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookRentalProject.Infrastructure.Interfaces.IRepository
{
    public interface IGenericRepo<T> where T : class
    {
        IEnumerable<T> GetAll(Expression <Func<T,bool>>? predicate = null,string? includeProperties=null );
        T GetByID(Expression<Func<T, bool>>? predicate , string? includeProperties = null);

        void Add(T Entity); 
        void Delete(int Id);
        void DeleteRange(IEnumerable<T> Entity);

    }
}
