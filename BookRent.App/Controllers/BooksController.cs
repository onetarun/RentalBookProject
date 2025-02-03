using System.Net;
using System.Text;
using BookRent.App.ViewModels;
using BookRent.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookRent.App.Controllers
{
    public class BooksController : Controller
    {
        private readonly HttpClient _httpClient;

        public BooksController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("MyAPIClient");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            List<VMBookList> vm = new List<VMBookList>();
            
            var books = new List<Book>();

            var response = await _httpClient.GetAsync("api/Books/GetAllBooksWithGenre");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                books = JsonConvert.DeserializeObject<List<Book>>(jsonString);
                vm = books.Select(g => new VMBookList 
                { BookID = g.BookID,
                ISBN = g.ISBN,
                Title = g.Title, 
                Availability = g.Availability,
                Price = g.Price,
                    GenreName = g.Genre.GenreCategory
                
                }).ToList();
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
                vm.Genres = genre.Select(g => new VMGenre { GenreID = g.GenreID, GenreCategory = g.GenreCategory }).ToList();
            }
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(VMBook book)
        {
            using (HttpClient client = new HttpClient())
            {
                book.BookImagePath = "abc";
                var json = JsonConvert.SerializeObject(book);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("api/Books", content);
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
            var response = await _httpClient.GetAsync($"api/Books/{Id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                book = JsonConvert.DeserializeObject<Book>(jsonString);

            }
            else
            {
                ViewBag.Error = "Unable to fetch Book.";
            }
            VMBook vm = new VMBook
            {
                    BookID = book.BookID,
                    ISBN = book.ISBN,
                    Title = book.Title,
                    Author = book.Author,
                    Description = book.Description,
                    BookImagePath = book.BookImagePath,
                    Availability = book.Availability,
                    Price = book.Price,
                    GenreID = book.GenreID,
                    PublisherName = book.PublisherName,
                    PublicationDate = book.PublicationDate,
                    TotalPages = book.TotalPages,
                    BookDimensions = book.BookDimensions
            };
            return View(vm);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(VMBook vm)
        {
            var books = new Book();
            var response = await _httpClient.GetAsync($"api/Books/{vm.BookID}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                books = JsonConvert.DeserializeObject<Book>(jsonString);
                books.BookID = vm.BookID;
                books.ISBN = vm.ISBN;
                books.Title = vm.Title;
                books.Author = vm.Author;
                books.Description = vm.Description;
                books.BookImagePath = vm.BookImagePath;
                books.Availability = vm.Availability;
                books.Price = vm.Price;
                books.GenreID = vm.GenreID;
                books.PublisherName = vm.PublisherName;
                books.PublicationDate = vm.PublicationDate;
                books.TotalPages = vm.TotalPages;
                books.BookDimensions = vm.BookDimensions;
            }
            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(books);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage responseMsg = await _httpClient.PutAsync("api/books", content);

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
    }
}
