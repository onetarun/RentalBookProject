using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookRent.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookRent.Application.Interfaces.IRepository
{
    public interface IBookRepo :  IGenericRepo<Book>
    {

        Task<IEnumerable<Book>> GetBooksByAuthorAsync(string author);
        Task<IEnumerable<Book>> GetBooksByGenreAsync(int genreId);

        Task UpdateBookAsync(Book book);
        
    }
}
