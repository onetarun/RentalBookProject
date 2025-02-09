using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BookRent.Application.Interfaces.IRepository
{
    public interface IUtilityRepo
    { 
        Task<string> SaveImage(IFormFile bookImage);
        Task DeleteImage(string bookUrl);
        Task<string> EditImage(string bookUrl, IFormFile bookImage);
        //Update Image
        Task<string> GetFileName(string bookUrl);

        //testing phase
    }
}
