using AutoMapper;
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
using System.Text;
namespace BookRent.App.Controllers
{
    public class GenreController : Controller
    {

        private readonly HttpClient _httpClient;

        private IMapper _mapper;

        public GenreController(IHttpClientFactory httpClient, IMapper mapper)
        {
            _httpClient = httpClient.CreateClient("MyAPIClient");
            _mapper = mapper;
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            List<VMGenre> genrevm = new List<VMGenre>();
            var genres = new List<Genre>();
            var response = await _httpClient.GetAsync("api/Genre");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                genres = JsonConvert.DeserializeObject<List<Genre>>(jsonString);

                genrevm = _mapper.Map(genres,genrevm);

               // genreViewModels =genres.Select(g => new VMGenre { GenreId = g.GenreID, Title = g.Title }).ToList();

            }
            else
            {
                ViewBag.Error = "Unable to fetch genres.";
            }           
                 
            return View(genrevm);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(VMGenre genres)
        {
            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(genres);
                var content = new StringContent(json, Encoding.UTF8, "application/json");       
                HttpResponseMessage response = await _httpClient.PostAsync("api/Genre", content);               
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {

            
            var genres = new Genre();
            var response = await _httpClient.GetAsync($"api/Genre/{Id}");
            
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                genres = JsonConvert.DeserializeObject<Genre>(jsonString);                
                
            }
            else
            {
                ViewBag.Error = "Unable to fetch genres.";
            }
            VMGenre viewModel = new VMGenre();
            viewModel = _mapper.Map(genres,viewModel);
             
            return View(viewModel);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(VMGenre genrevm)
        {
            var genres = new Genre();
            var response = await _httpClient.GetAsync($"api/Genre/{genrevm.GenreId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                genres = JsonConvert.DeserializeObject<Genre>(jsonString);
                //genres.Title = genrevm.Title;

                genrevm = _mapper.Map(genres, genrevm);
            }
            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(genres);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage responseMsg = await _httpClient.PutAsync($"api/Genre/{genrevm.GenreId}", content);

                if (responseMsg.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }


            return RedirectToAction("Index");           

        }

        //[HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            var response = await _httpClient.DeleteAsync($"api/Genre/{Id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.Error = "Unable to fetch genres.";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var genres = new List<Genre>();            
            var response = await _httpClient.GetAsync("api/Genre");
            return View(response);

        }

        //[HttpGet]
        //public async Task<IActionResult> GetGenreById()
        //{
        //    var genres = new List<Genre>();
        //    var response = await _httpClient.GetAsync($"api/Genre/{Id}");
        //    var response = await _httpClient.GetAsync("api/Genre");
        //    return View(response);

        //}
    }
}
