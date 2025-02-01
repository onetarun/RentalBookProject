using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookRent.Application.Interfaces.IRepository
{
    public interface IApplicationdbContext
    {
        


      
            DbSet<T> Set<T>() where T : class; // To allow DbSet access for any entity.
           // Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
      


    }
}
