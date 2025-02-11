using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookRent.Domain.Entities;
using BookRent.Domain.ViewModels;



namespace BookRent.Application.Interfaces.IRepository
{
    public interface IUserService : IGenericRepo<UserRegisteration>
    {
        //Task<bool> Register(UserRegisteration user);
        //Task<string> Authenticate(string email, string password);


        Task<ResponseModel> Register(UserRegisteration user);
        Task<ResponseModel> Authenticate(string email, string password);
    }
}
