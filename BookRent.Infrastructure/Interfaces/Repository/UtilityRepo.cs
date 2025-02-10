using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookRent.Application.Interfaces.IRepository;
using BookRent.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BookRent.Infrastructure.Interfaces.Repository
{
    public class UtilityRepo : IUtilityRepo
    {
        string ContainerName = "BookImage";

        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _contextAccessor;

        public UtilityRepo(IWebHostEnvironment environment, IHttpContextAccessor contextAccessor)
        {
            _environment = environment;
            _contextAccessor = contextAccessor;
        }

        public Task DeleteImage(string bookUrl)
        {
            if (string.IsNullOrEmpty(bookUrl))
            {
                return Task.CompletedTask;
            }
            var filename = Path.GetFileName(bookUrl);
            var completePath = Path.Combine(_environment.WebRootPath, ContainerName, filename);
            if (File.Exists(completePath))
            {
                File.Delete(completePath);
            }
            return Task.CompletedTask;
        }

        public async Task<string> EditImage(string bookUrl, IFormFile bookImage)
        {
            await DeleteImage(bookUrl);
            return await SaveImage(bookImage);
        }

        public async Task<string> SaveImage(IFormFile bookImage)
        {
            var extention = Path.GetExtension(bookImage.FileName);
            var filename = $"{Guid.NewGuid()}{extention}";
            string folder = Path.Combine(_environment.WebRootPath, ContainerName);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string filepath = Path.Combine(folder, filename);
            using (var memorystream = new MemoryStream())
            {
                await bookImage.CopyToAsync(memorystream);
                var content = memorystream.ToArray();
                await File.WriteAllBytesAsync(filepath, content);
            }
            var basePath = $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}";
            var completePath = Path.Combine(basePath, ContainerName, filename).Replace("\\", "/");

            return completePath;
        }

        public async Task<string> GetFileName(string bookUrl)
        {
            if (string.IsNullOrEmpty(bookUrl))
            {
                return null;
            }
            var filename = Path.GetFileName(bookUrl);
            var completePath = Path.Combine(_environment.WebRootPath, ContainerName, filename);
            return completePath;
        }
    }
}
