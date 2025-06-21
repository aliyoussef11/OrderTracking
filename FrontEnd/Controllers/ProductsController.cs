using FrontEnd.Models;
using FrontEnd.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace FrontEnd.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient;

        #region Ctor
        public ProductsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("DefaultClient");
        }
        #endregion

        public async Task<IActionResult> Index()
        {
            // Get the token
            var token = Request.Cookies["access_token"];
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("/api/products");

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Error wile fetching products.");

                return View();
            }

            var products = await response.Content.ReadFromJsonAsync<List<ProductDTO>>();

            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDTO)
        {
            // 1) Call the gateway
            var response = await _httpClient.PostAsJsonAsync("/api/products", productDTO);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Unable to create the product.");

                return View(productDTO);
            }

            TempData["SuccessMessage"] = "Products created successfully!";
            return RedirectToAction("Index", "Products");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            // 1) Call the gateway
            var response = await _httpClient.DeleteAsync($"/api/products/{id}");

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Unable to delete the product.");

                return RedirectToAction("Index", "Products");
            }

            TempData["SuccessMessage"] = "Product deleted successfully!";
            return RedirectToAction("Index", "Products");
        }
    }
}
