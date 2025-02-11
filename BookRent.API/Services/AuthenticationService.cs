using BookRent.API.Data;
using BookRent.API.DTOs;
using BookRent.API.Models;
using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookRent.API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AuthenticationAPIContext _context;
        private readonly ITokenService _tokenService;

        public AuthenticationService(UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            AuthenticationAPIContext context, 
            ITokenService tokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDTO> UserLogin(LoginDTO loginDTO)
        {
            //var user = _context.ApplicationUsers.FirstOrDefault(x => x.Email == loginDTO.Email);
            var user = _context.ApplicationUsers.FirstOrDefault(x => x.UserName.ToLower() == loginDTO.UserName.ToLower());
            
            if (user != null)
            {
                var IsValid = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
                if (IsValid)
                {
                    var userInfo = new UserInfoDTO
                    {
                        Id = user.Id,
                        Name = user.UserName,
                        Email = user.Email,                            
                        PhoneNumber = user.PhoneNumber
                    };
                    return new LoginResponseDTO
                    {
                        Token = await _tokenService.GenerateToken(user),
                        UserInfo = userInfo
                    };
                }

            }
            return new LoginResponseDTO { Token = "", UserInfo = null };
        }

        public async Task<string> UserRegister(UserRegisterationDTO userData)
        {
            var user = new ApplicationUser
            {
                UserName = userData.Email,
                Email = userData.Email,
                NormalizedEmail = userData.Email.ToUpper(),
                PhoneNumber = userData.Phone,
            };

            var result = await _userManager.CreateAsync(user, userData.Password);
            
            if (result.Succeeded)
            {
                return "";
            }
            else
            {
                //return result.Errors.Select(x => x.Description).FirstOrDefault();
                return result.Errors.FirstOrDefault().Description;
            }            
        }
    }
}
