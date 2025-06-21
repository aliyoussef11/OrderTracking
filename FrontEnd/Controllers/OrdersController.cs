using FrontEnd.Models;
using FrontEnd.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace FrontEnd.Controllers
{
    public class OrdersController : Controller
    {
        private readonly HttpClient _httpClient;

        #region Ctor
        public OrdersController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("DefaultClient");
        }
        #endregion

        public async Task<IActionResult> Index()
        {
            // Get the token
            var token = Request.Cookies["access_token"];
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("/api/orders");

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Error wile fetching orders.");

                return View();
            }

            var orders = await response.Content.ReadFromJsonAsync<List<OrderDTO>>();

            if(orders != null && orders.Any())
                foreach(var order in orders)
                {
                    var productResponse = await _httpClient.GetAsync($"/api/products/{order.ProductId}");
                    if(productResponse != null)
                    {
                        var product = await productResponse.Content.ReadFromJsonAsync<ProductDTO>();

                        if (product != null)
                            order.ProductName = product.Name;
                    }                 
                }
            

            return View(orders);
        }


        public async Task<IActionResult> Create()
        {
            var token = Request.Cookies["access_token"];
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            // get all the products to show in the selector
            var response = await _httpClient.GetAsync("/api/products");
            var products = await response.Content.ReadFromJsonAsync<List<ProductDTO>>();


            return View("CreateOrder", new CreateOrderViewModel
            {
                Products = products
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderViewModel viewModel)
        {
            // 1) Call the gateway
            var token = Request.Cookies["access_token"];
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsJsonAsync("/api/orders", viewModel.Order);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Unable to create the order.");

                return View(viewModel);
            }

            TempData["SuccessMessage"] = "Order created successfully!";
            return RedirectToAction("Index", "Orders");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            // 1) Call the gateway
            var token = Request.Cookies["access_token"];
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.DeleteAsync($"/api/orders/{id}");

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Unable to delete the order.");

                return RedirectToAction("Index", "Orders");
            }

            TempData["SuccessMessage"] = "Order deleted successfully!";
            return RedirectToAction("Index", "Orders");
        }
    }
}
