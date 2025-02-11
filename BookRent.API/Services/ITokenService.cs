using BookRent.API.Models;
using Microsoft.AspNetCore.Identity;

namespace BookRent.API.Services
{
    public interface ITokenService
    {
        Task<string> GenerateToken(ApplicationUser user);

        Task<string> GenerateRefreshTokenAsync();

    }
}
