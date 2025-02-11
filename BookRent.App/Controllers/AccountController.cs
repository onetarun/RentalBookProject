using System.Text;
using BookRent.App.ViewModels;
using BookRent.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookRent.App.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("MyAPIClient");

        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(VMUserRegisteration user)
        {

            if (string.IsNullOrEmpty(user.Role)) user.Role = "User";
            if (user.Permissions == null || user.Permissions.Count == 0)
            {
                user.Permissions = new List<string> { "ALL" };
            }

            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(user);

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("api/auth/register", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(VMUserRegisteration user)
        {
            if (string.IsNullOrEmpty(user.Role)) user.Role = "User";
            if (user.Permissions == null || user.Permissions.Count == 0)
            {
                user.Permissions = new List<string> { "ALL" };
            }
            user.Name = "ADMIN";
            using (HttpClient client = new HttpClient())
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/login", user);
                if (response.IsSuccessStatusCode) return RedirectToAction("Index", "Home");
                ModelState.AddModelError("", "Invalid Credentials");

                return RedirectToAction("Index", "Home");
            }
        }

    }
}
