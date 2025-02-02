using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookRent.Domain.Entities;

namespace BookRent.Application.Interfaces.IRepository
{
        public interface IGenreRepository : IGenericRepo<Genre>
        {
            bool IsExists(int id);
            Task UpdategenreAsync(Genre genre);
        }
 }

