using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookRent.Domain.Entities;

namespace BookRent.Application.Interfaces.IRepository
{
    public interface IUtilityRepo
    { 
        Task<string> SaveImage(Book book);
        Task DeleteImage(Book book);
        Task<string> EditImage(Book book);
        //testing phase
    }
}
