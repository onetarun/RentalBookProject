using BookRent.API.DTOs;

namespace BookRent.API.Services
{
    public interface IAuthenticationService
    {
        Task<string> UserRegister(UserRegisterationDTO userData);
        Task<LoginResponseDTO> UserLogin(LoginDTO loginDTO);
    }
}
