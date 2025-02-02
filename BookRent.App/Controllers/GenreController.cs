using BookRent.App.ViewModels;
using BookRent.Application.Interfaces.IRepository;
using BookRent.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
namespace BookRent.App.Controllers
{
    public class GenreController : Controller
    {

        private readonly HttpClient _httpClient;
        //private readonly IUnitOfWork _unitOfWork;

        //public GenreController(IUnitOfWork unitOfWork, IHttpClientFactory httpClient)
        //{
        //    _unitOfWork = unitOfWork;
        //    _httpClient = httpClient.CreateClient("MyAPIClient");
        //}       

        public GenreController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("MyAPIClient");
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            List<VMGenre> genreViewModels = new List<VMGenre>();
            var genres = new List<Genre>();
            var response = await _httpClient.GetAsync("api/Genre");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                genres = JsonConvert.DeserializeObject<List<Genre>>(jsonString);
                genreViewModels=genres.Select(g => new VMGenre { GenreID = g.GenreID, GenreCategory = g.GenreCategory }).ToList();

            }
            else
            {
                ViewBag.Error = "Unable to fetch genres.";
            }           
                 
            return View(genreViewModels);
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
