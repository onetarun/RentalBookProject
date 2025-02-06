using System.Net;
using System.Text;
using AutoMapper;
using BookRent.App.ViewModels;
using BookRent.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookRent.App.Controllers
{
    public class BooksController : Controller
    {
        private readonly HttpClient _httpClient;
        private IMapper _mapper;
        public BooksController(IHttpClientFactory httpClient, IMapper mapper)
        {
            _httpClient = httpClient.CreateClient("MyAPIClient");
            _mapper = mapper;
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

                vm=_mapper.Map(books,vm);

                //vm = books.Select(g => new VMBookList 
                //{ BookId = g.BookId,
                //ISBN = g.ISBN,
                //Title = g.Title, 
                //IsAvailable = g.IsAvailable,
                //Price = g.Price,
                //GenreName = g.Genre.Title
                
                //}).ToList();
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

                vm.Genres=_mapper.Map(genre,vm.Genres);

                //vm.Genres = genre.Select(g => new VMGenre { GenreId = g.GenreID, Title = g.Title }).ToList();
            }
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(VMBook book)
        {
            using (HttpClient client = new HttpClient())
            {
                book.BookUrl = "abc";
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
            VMBook vm = new VMBook();

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                book = JsonConvert.DeserializeObject<Book>(jsonString);

                vm = _mapper.Map(book, vm);
            }
            else
            {
                ViewBag.Error = "Unable to fetch Book.";
            }
            //VMBook vm = new VMBook
            //{
            //        BookId = book.BookId,
            //        ISBN = book.ISBN,
            //        Title = book.Title,
            //        Author = book.Author,
            //        Description = book.Description,
            //        BookImagePath = book.BookUrl,
            //        Availability = book.IsAvailable,
            //        Price = book.Price,
            //        GenreId = book.GenreId,
            //        PublisherName = book.PublisherName,
            //        PublicationDate = book.PublicationDate,
            //        TotalPages = book.TotalPages,
            //        BookDimensions = book.BookDimensions
            //};
            return View(vm);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(VMBook vm)
        {
            var books = new Book();
            var response = await _httpClient.GetAsync($"api/Books/{vm.BookId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                books = JsonConvert.DeserializeObject<Book>(jsonString);

                books = _mapper.Map(vm,books);

                //books.BookId = vm.BookId;
                //books.ISBN = vm.ISBN;
                //books.Title = vm.Title;
                //books.Author = vm.Author;
                //books.Description = vm.Description;
                //books.BookUrl = vm.BookImagePath;
                //books.IsAvailable = vm.Availability;
                //books.Price = vm.Price;
                //books.GenreId = vm.GenreId;
                //books.PublisherName = vm.PublisherName;
                //books.PublicationDate = vm.PublicationDate;
                //books.TotalPages = vm.TotalPages;
                //books.BookDimensions = vm.BookDimensions;
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
