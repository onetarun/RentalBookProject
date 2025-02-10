using System.Linq.Expressions;
using System.Net;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public BooksController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

            var bookDto = _mapper.Map<List<BookDTO>>(books);
            return Ok(bookDto);
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

            var bookDto = _mapper.Map<List<BookListDTO>>(books);
            return Ok(bookDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var book = await _unitOfWork.Book.GetByIDWithGenre(id);

                if (book == null)
                {
                    return NotFound($"Book with ID {id} not found.");
                }
                var bookDto = _mapper.Map<BookDTO>(book);
                return Ok(bookDto); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditBook(int id, [FromBody] BookDTO dto)
        {
            if (id != dto.BookId)
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
                book = _mapper.Map(dto, book);

                await _unitOfWork.Book.UpdateBookAsync(book);

                return Ok("Book updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("AddBook")]
        public IActionResult AddBook(BookDTO dto)
        {
            if (dto == null)
            {
                return  BadRequest("Book details are missing.");
            }

            try
            {
                Book book = new Book();

                book = _mapper.Map(dto, book);

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
