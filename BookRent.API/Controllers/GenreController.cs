using System.Linq.Expressions;
using BookRent.API.DTOs;
using BookRent.Application.Interfaces.IRepository;
using BookRent.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace BookRent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

        public GenreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres([FromQuery] string? filter = null)
        {
            // Define the predicate based on the filter
            Expression<Func<Genre, bool>>? predicate = null;
            if (!string.IsNullOrEmpty(filter))
            {
                predicate = g => g.GenreCategory.Contains(filter);
            }

            // No include in this example, but you can pass one if needed
            Func<IQueryable<Genre>, IIncludableQueryable<Genre, object>>? include = null;

            // Call the repository method
            var genres = await _unitOfWork.Genre.GetAllAsync(predicate, include);

            if (genres == null || !genres.Any())
            {
                return NotFound("No genres found.");
            }

            return Ok(genres);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id, [FromBody] GenreDTO genreDto)
        {
            if (id != genreDto.GenreID)
            {
                return BadRequest("Genre ID mismatch.");
            }

            try
            {
                var genre = await _unitOfWork.Genre.GetByIdAsync(id);
                if (genre == null)
                {
                    return NotFound($"Genre with ID {id} not found.");
                }

                // Map DTO to entity
                genre.GenreCategory = genreDto.GenreCategory;
                genre.GenreID = id;
                await _unitOfWork.Genre.UpdategenreAsync(genre);

                return Ok("Genre updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var genre = await _unitOfWork.Genre.GetByIdAsync(id);
            if (genre == null)
            {
                return NotFound("Genre not found.");
            }

            try
            {
                 _unitOfWork.Genre.Delete(id);
                // _unitOfWork.SaveChangesAsync();
                return Ok("Genre deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddGenre([FromBody] GenreDTO genreDto)
        {
            if (genreDto == null)
            {
                return BadRequest("Genre data is required.");
            }

            try
            {
                var genre = new Genre
                {
                    GenreCategory = genreDto.GenreCategory

                };

                 _unitOfWork.Genre.Add(genre);


                return Ok("Genre Added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            try
            {
                var genre = await _unitOfWork.Genre.GetByIdAsync(id);

                if (genre == null)
                {
                    return NotFound($"Genre with ID {id} not found.");
                }

                return Ok(genre);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



    }
}
