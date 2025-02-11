using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookRent.Domain.ViewModels;

namespace BookRent.Application.Interfaces.IRepository
{
    public interface IHttpService
    {
        Task<ResponseModel> SendData(ClientRequest requestData, bool Token = true);

    }
}
