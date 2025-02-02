using System.Linq.Expressions;
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


    }
}
