using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookRent.Application.Interfaces.IRepository;
using BookRent.Domain.Entities;
using BookRent.Domain.ViewModels;
using BookRent.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookRent.Infrastructure.Interfaces.Repository
{
    public class UserService : GenericRepo<UserRegisteration>, IUserService
    {

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        //public UserService(ApplicationDbContext context, IConfiguration configuration)
        //{
        //    _context = context;
        //    _configuration = configuration;
        //}

       // private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _configuration = configuration;
        }


        public async Task<ResponseModel> Register(UserRegisteration user)
        {
            if (_context.Users.Any(u => u.Email == user.Email))
                return new ResponseModel { IsSuccess = false, Message = "Email already in use" };

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new ResponseModel { IsSuccess = true, Message = "User Registered Successfully", Result = user };
        }

        public async Task<ResponseModel> Authenticate(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return new ResponseModel { IsSuccess = false, Message = "Invalid Credentials" };

            return new ResponseModel { IsSuccess = true, Message = "Login Successful",
                Result = GenerateJwtToken(user) };
        }

        private string GenerateJwtToken(UserRegisteration user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role)
        };
            foreach (var permission in user.Permissions)
            {
                claims.Add(new Claim("Permission", permission));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        //public Task<IReadOnlyList<UserRegisteration>> GetAllAsync(Expression<Func<UserRegisteration, bool>>? predicate, Func<IQueryable<UserRegisteration>, IIncludableQueryable<UserRegisteration, object>> include)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<UserRegisteration?> GetByIdAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Add(UserRegisteration Entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Delete(int Id)
        //{
        //    throw new NotImplementedException();
        //}

        //public void DeleteRange(IEnumerable<UserRegisteration> Entity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}