using System.Linq.Expressions;
using AutoMapper;
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
        private IMapper _mapper;

        public GenreController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres([FromQuery] string? filter = null)
        {
            // Define the predicate based on the filter
            Expression<Func<Genre, bool>>? predicate = null;
            if (!string.IsNullOrEmpty(filter))
            {
                predicate = g => g.Title.Contains(filter);
            }

            // No include in this example, but you can pass one if needed
            Func<IQueryable<Genre>, IIncludableQueryable<Genre, object>>? include = null;

            // Call the repository method
            var genres = await _unitOfWork.Genre.GetAllAsync(predicate, include);

            if (genres == null || !genres.Any())
            {
                return NotFound("No genres found.");
            }

            var genreDto=_mapper.Map<List<GenreDTO>>(genres);
            return Ok(genreDto);
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

                genre = _mapper.Map(genreDto, genre);

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
                Genre genre = new Genre();

                genre = _mapper.Map(genreDto, genre);

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
                var genreDto = _mapper.Map<GenreDTO>(genre);
                return Ok(genreDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



    }
}
