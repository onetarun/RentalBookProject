using BookRent.API.DTOs;
using BookRent.API.Services;
using Microsoft.AspNetCore.Mvc;


namespace BookRent.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private ResponseDTO _responseDTO;

        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
            _responseDTO = new ResponseDTO();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterationDTO registerDto)
        {
            var response = await _authService.UserRegister(registerDto);
            if (!string.IsNullOrEmpty(response))
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = response;
                return BadRequest(_responseDTO);
            }
            _responseDTO.Message = "User Created";
            return Ok(_responseDTO);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var response = await _authService.UserLogin(loginDTO);

            if (response.UserInfo == null)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = "UserName and Password Incorrect";
                return BadRequest(_responseDTO);
            }

            _responseDTO.Result = response;
            return Ok(_responseDTO);
        }
    }
}
