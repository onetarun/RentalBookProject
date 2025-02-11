using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRent.Domain.ViewModels
{
    public class ClientRequest
    {
      
            public string Url { get; set; }
            public object Data { get; set; }
            public string AccessToken { get; set; }
            public APIType ApiType { get; set; } = APIType.Get;
            public ContentType ContentType { get; set; } = ContentType.Json;


        }
        public enum ContentType
        {
            Json, MultipartFormData
        }
        public enum APIType
        {
            Get, Post, Put, Delete
        }

    
}
