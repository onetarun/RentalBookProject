using AutoMapper;
using BookRent.API.DTOs;
using BookRent.Application.Interfaces.IRepository;
using BookRent.Domain.Entities;
using BookRent.Infrastructure.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookRent.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public AuthController(IUnitOfWork userService, IMapper mapper) {
            _unitOfWork = userService; _mapper = mapper; }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterationDTO userRegisterationDTO)
        {
            UserRegisteration userNew = new UserRegisteration();

            userNew = _mapper.Map(userRegisterationDTO, userNew);

            if (string.IsNullOrEmpty(userNew.Role)) userNew.Role = "User";
            /*if (string.IsNullOrEmpty(userNew.Permissions.Count)) userNew.Permissions = "ALL";*/// Default role
            if (userNew.Permissions == null || userNew.Permissions.Count == 0)
            {
                userNew.Permissions = new List<string> { "ALL" };
            }
            var response = await _unitOfWork.UserService.Register(userNew);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserRegisteration loginUser)
        {
            var response = await _unitOfWork.UserService.Authenticate(loginUser.Email, loginUser.PasswordHash);
            return response.IsSuccess ? Ok(response) : Unauthorized(response);
        }

    }
}
