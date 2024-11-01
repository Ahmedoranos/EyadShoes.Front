using EyadShoes.Front.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace EyadShoes.Front.Controllers
{
    public class AccountController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }



        public IActionResult Login()
        {
            return View();
        }


        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel model)
        {
            var client = _httpClientFactory.CreateClient("AnotherProjectClient");

            var jsonContent = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("Authentications/SignUp", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home"); // Redirect on successful signup
            }

            ModelState.AddModelError(string.Empty, "An error occurred during sign up.");
            return View(model);
        }
    }
}
