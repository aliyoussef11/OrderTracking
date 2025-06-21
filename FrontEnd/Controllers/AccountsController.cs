using FrontEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class AccountsController : Controller
    {
        private readonly HttpClient _httpClient;

        #region Ctor
        public AccountsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("DefaultClient");
        }
        #endregion

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            // 1) Call the gateway
            var response = await _httpClient.PostAsJsonAsync("/api/auth/register", registerDTO);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Registration failed. Please try again.");

                return View(registerDTO);
            }

            TempData["SuccessMessage"] = "Registration successful!";
            return RedirectToAction("Login", "Accounts");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            // 1) Call the gateway
            var response = await _httpClient.PostAsJsonAsync("/api/auth/login", loginDTO);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Invalid Username of password.");

                return View(loginDTO);
            }

            var tokenExpiration = DateTime.UtcNow.AddMinutes(90);
            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

            Response.Cookies.Append("access_token", loginResponse.token, new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = tokenExpiration
            });

            return RedirectToAction("Index", "Home");
        }

    }
}
