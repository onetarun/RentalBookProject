using System.Net;
using System.Text;
using AutoMapper;
using BookRent.App.ViewModels;
using BookRent.Application.Interfaces.IRepository;
using BookRent.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookRent.App.Controllers
{
    public class BooksController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IUtilityRepo _utility;
        private IMapper _mapper;
        public BooksController(IHttpClientFactory httpClient, IMapper mapper, IUtilityRepo utility)
        {
            _httpClient = httpClient.CreateClient("MyAPIClient");
            _mapper = mapper;
            _utility = utility;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            List<VMBookList> vm = new List<VMBookList>();
            
            var books = new List<VMBookList>();

            var response = await _httpClient.GetAsync("api/Books/GetAllBooksWithGenre");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                vm = JsonConvert.DeserializeObject<List<VMBookList>>(jsonString);

                //vm=_mapper.Map(books,vm);
            }
            else
            {
                ViewBag.Error = "Unable to fetch Books.";
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            VMBook vm = new VMBook();
            var genre = new List<Genre>();

            var response = await _httpClient.GetAsync("api/Genre");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                genre = JsonConvert.DeserializeObject<List<Genre>>(jsonString);

                vm.Genre=_mapper.Map(genre,vm.Genre);

            }
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(VMBook vm)
        {
            if (vm.BookImage != null)
            {
                vm.BookUrl = await _utility.SaveImage(vm.BookImage);
            }
            var genres = new VMGenre();
            var response1 = await _httpClient.GetAsync($"api/Genre/{vm.GenreId}");

            if (response1.IsSuccessStatusCode)
            {
                var jsonString = await response1.Content.ReadAsStringAsync();
                genres = JsonConvert.DeserializeObject<VMGenre>(jsonString); 
            } 

            vm.Genre=new List<VMGenre> { genres };

            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(vm);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("api/Books/AddBook", content);
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
            var book = new Book();
            List<Genre> genrevm = new List<Genre>();
            
            VMBook vm = new VMBook(); 

            var response = await _httpClient.GetAsync($"api/Books/{Id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                vm = JsonConvert.DeserializeObject<VMBook>(jsonString); 

                var GenreResponse = await _httpClient.GetAsync("api/Genre");
                if (GenreResponse.IsSuccessStatusCode)
                {
                    var jsonString1 = await GenreResponse.Content.ReadAsStringAsync();
                    genrevm = JsonConvert.DeserializeObject<List<Genre>>(jsonString1);

                    vm.Genre = _mapper.Map(genrevm, vm.Genre); 
                }
            }
            else
            {
                ViewBag.Error = "Unable to fetch Book.";
            }

            return View(vm);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(VMBook vm)
        {
            if (vm.BookImage != null)
            {
                vm.BookUrl = await _utility.EditImage(vm.BookUrl, vm.BookImage);
            }

            var books = new VMBook();
            var response = await _httpClient.GetAsync($"api/Books/{vm.BookId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                books = JsonConvert.DeserializeObject<VMBook>(jsonString);

                //books = _mapper.Map(vm,books);

            }
            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(vm);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage responseMsg = await _httpClient.PutAsync($"api/books/{vm.BookId}", content);

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
            var response = await _httpClient.DeleteAsync($"api/Books/{Id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Unable to fetch Book.";
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> BookGallery()
        {

            List<VMBookGallery> vm = new List<VMBookGallery>();
            var book = new List<Book>();

            var response = await _httpClient.GetAsync("api/Books");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                vm = JsonConvert.DeserializeObject<List<VMBookGallery>>(jsonString);

            }
            else
            {
                ViewBag.Error = "Unable to fetch Books.";
            }

            return View(vm);
        }

        public async Task<IActionResult> BookDetails(int id)
        {

            VMBookDetails vm = new VMBookDetails();
            var book = new List<Book>();

            var response = await _httpClient.GetAsync($"api/Books/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                vm = JsonConvert.DeserializeObject<VMBookDetails>(jsonString);

            }
            else
            {
                ViewBag.Error = "Unable to fetch Books.";
            }

            return View(vm);
        }
    }
}
