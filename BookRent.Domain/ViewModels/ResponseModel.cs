using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRent.Domain.ViewModels
{
    public class ResponseModel
    {
        

        public object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }
    }
}
