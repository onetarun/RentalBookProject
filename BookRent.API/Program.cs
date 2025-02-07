

using BookRent.API.Mapper;
using BookRent.Application.Interfaces.IRepository;
using BookRent.Infrastructure.Data;
using BookRent.Infrastructure.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookRentAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var config = new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutomapperProfile());
            });
            var mapper = config.CreateMapper();
            builder.Services.AddSingleton(mapper);

            builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // Middleware Pipeline
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
