using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;
using ProductService.Infrastructure.Persistence.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _productDbContext;

        #region Ctor
        public ProductRepository(ProductDbContext context)
        {
            _productDbContext = context;
        }
        #endregion

        #region Public Functions
        public async Task AddProductAsync(Product product)
        {
            await _productDbContext.Products.AddAsync(product);
            await _productDbContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var product = await _productDbContext.Products.FindAsync(id);
            if (product != null)
            {
                _productDbContext.Products.Remove(product);
                await _productDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productDbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            return await _productDbContext.Products.FirstOrDefaultAsync(prod => prod.Id == id);
        }

        public async Task UpdateProductAsync(Product product)
        {
            _productDbContext.Products.Update(product);
            await _productDbContext.SaveChangesAsync();
        }
        #endregion
    }
}
