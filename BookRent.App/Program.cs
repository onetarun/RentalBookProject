using Microsoft.Extensions.Options;
using BookRent.App.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<MyApiSettings>(builder.Configuration.GetSection("MyApiSettings"));

builder.Services.AddHttpClient("MyAPIClient", (serviceProvider, client) =>
{
    var appSettings = serviceProvider.GetRequiredService<IOptions<MyApiSettings>>().Value;
    client.BaseAddress = new Uri(appSettings.BaseAddress);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
