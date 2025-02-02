using BookRent.Application.Interfaces.IRepository;
using BookRent.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net.Http;
namespace BookRent.App.Controllers
{
    public class GenreController : Controller
    {

        private readonly HttpClient _httpClient;
        private readonly IGenreRepository _genreRepository;

        public GenreController(IGenreRepository genreRepository, IHttpClientFactory httpClient)
        {
            _genreRepository = genreRepository;
            _httpClient = httpClient.CreateClient("MyAPIClient");
        }       

              
        //[Authorize]
        
        public async Task<IActionResult> Index()
        {
            //var response = await _genreRepository.GetAllAsync();
            var response = await _httpClient.GetAllAsync("api/Genre");
            if (response.IsSuccess)
            {

            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var genres = new List<Genre>();
            var response = await _httpClient.GetAsync("api/Genre");
            return View(response);

        }
    }
}
