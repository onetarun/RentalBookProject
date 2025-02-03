using System.Linq.Expressions;
using System.Net;
using BookRent.API.DTOs;
using BookRent.Application.Interfaces.IRepository;
using BookRent.Domain.Entities;
using BookRent.Infrastructure.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;

namespace BookRent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBooks([FromQuery] string? filter = null)
        {
            // Define the predicate based on the filter
            Expression<Func<Book, bool>>? predicate = null;
            if (!string.IsNullOrEmpty(filter))
            {
                predicate = g => g.PublisherName.Contains(filter);
                predicate = g => g.Author.Contains(filter);
            }

            // No include in this example, but you can pass one if needed
            Func<IQueryable<Book>, IIncludableQueryable<Book, object>>? include = null;

            // Call the repository method
            var books = await _unitOfWork.Book.GetAllAsync(predicate, include); 

            if (books == null || !books.Any())
            {
                return NotFound("No genres found.");
            }

            return Ok(books);
        }
        [HttpGet("GetAllBooksWithGenre")]
        public async Task<IActionResult> GetAllBooksWithGenre([FromQuery] string? filter = null)
        {
            // Define the predicate based on the filter
            Expression<Func<Book, bool>>? predicate = null;
            if (!string.IsNullOrEmpty(filter))
            {
                predicate = g => g.PublisherName.Contains(filter);
                predicate = g => g.Author.Contains(filter);
            }

            // No include in this example, but you can pass one if needed
            Func<IQueryable<Book>, IIncludableQueryable<Book, object>>? include = null;

            // Call the repository method
            var books = await _unitOfWork.Book.GetALLBooksWithGenre();

            if (books == null || !books.Any())
            {
                return NotFound("No genres found.");
            }

            return Ok(books);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var book = await _unitOfWork.Book.GetByIdAsync(id);

                if (book == null)
                {
                    return NotFound($"Book with ID {id} not found.");
                }

                return Ok(book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditBook(int id, [FromBody] BookDTO dto)
        {
            if (id != dto.BookID)
            {
                return BadRequest("Book ID mismatch.");
            }

            try
            {
                var book = await _unitOfWork.Book.GetByIdAsync(id);
                if (book == null)
                {
                    return NotFound($"Book with ID {id} not found.");
                }

                // Map DTO to entity
                book.BookID = dto.BookID;
                book.ISBN = dto.ISBN;
                book.Title = dto.Title;
                book.Author = dto.Author;
                book.Description = dto.Description;
                book.BookImagePath = dto.BookImagePath;
                book.Availability = dto.Availability;
                book.Price = dto.Price;
                book.GenreID = dto.GenreID;
                book.PublisherName = dto.PublisherName;
                book.PublicationDate = dto.PublicationDate;
                book.TotalPages = dto.TotalPages;
                book.BookDimensions = dto.BookDimensions;

                await _unitOfWork.Book.UpdateBookAsync(book);

                return Ok("Book updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddBook(BookDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Book details are missing.");
            }

            try
            {
                var book = new Book
                {
                    BookID = dto.BookID,
                    ISBN = dto.ISBN,
                    Title = dto.Title,
                    Author = dto.Author,
                    Description = dto.Description,
                    BookImagePath = dto.BookImagePath,
                    Availability = dto.Availability,
                    Price = dto.Price,
                    GenreID = dto.GenreID,
                    PublisherName = dto.PublisherName,
                    PublicationDate = dto.PublicationDate,
                    TotalPages = dto.TotalPages,
                    BookDimensions = dto.BookDimensions

                };

                _unitOfWork.Book.Add(book); 

                return Ok("Book Added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var book = await _unitOfWork.Book.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound("Book not found.");
            }

            try
            {
                _unitOfWork.Book.Delete(id); 
                return Ok("Book deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
