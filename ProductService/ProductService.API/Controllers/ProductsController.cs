using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.UseCases.Interfaces;
using Sahred.Common.DTOs;
using Microsoft.Extensions.Logging;

namespace ProductService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ICRUDProductUseCase _iCRUDProductUseCase;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ICRUDProductUseCase iCRUDProductUseCase, ILogger<ProductsController> logger)
        {
            _iCRUDProductUseCase = iCRUDProductUseCase;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Request received: Get all products");

            try
            {
                var products = await _iCRUDProductUseCase.GetAllAsync();
                _logger.LogInformation("Retrieved {Count} products", products.Count);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving products");
                return StatusCode(500, "An error occurred while retrieving products.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductDTO productDto)
        {
            _logger.LogInformation("Request received: Add product {@Product}", productDto);

            try
            {
                var success = await _iCRUDProductUseCase.AddAsync(productDto);

                if (success)
                {
                    _logger.LogInformation("Product added successfully: {@Product}", productDto);
                    return Ok("Product added successfully.");
                }
                else
                {
                    _logger.LogWarning("Failed to add product: {@Product}", productDto);
                    return BadRequest("Failed to add product.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while adding product: {@Product}", productDto);
                return StatusCode(500, "An error occurred while adding the product.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] ProductDTO productDto)
        {
            _logger.LogInformation("Request received: Update product {@Product}", productDto);

            try
            {
                var success = await _iCRUDProductUseCase.UpdateAsync(productDto);

                if (success)
                {
                    _logger.LogInformation("Product updated successfully: {@Product}", productDto);
                    return Ok("Product updated successfully.");
                }
                else
                {
                    _logger.LogWarning("Failed to update product: {@Product}", productDto);
                    return BadRequest("Failed to update product.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while updating product: {@Product}", productDto);
                return StatusCode(500, "An error occurred while updating the product.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Request received: Delete product {ProductId}", id);

            try
            {
                var success = await _iCRUDProductUseCase.DeleteAsync(id);

                if (success)
                {
                    _logger.LogInformation("Product deleted successfully: {ProductId}", id);
                    return Ok("Product deleted successfully.");
                }
                else
                {
                    _logger.LogWarning("Failed to delete product: {ProductId}", id);
                    return BadRequest("Failed to delete product.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while deleting product: {ProductId}", id);
                return StatusCode(500, "An error occurred while deleting the product.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            _logger.LogInformation("Request received: Get product by ID {ProductId}", id);

            try
            {
                var product = await _iCRUDProductUseCase.GetProductById(id);

                if (product is null)
                {
                    _logger.LogWarning("Product not found: {ProductId}", id);
                    return NotFound($"Product {id} not found");
                }

                _logger.LogInformation("Product retrieved successfully: {@Product}", product);
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while getting product: {ProductId}", id);
                return StatusCode(500, "An error occurred while retrieving the product.");
            }
        }
    }
}
