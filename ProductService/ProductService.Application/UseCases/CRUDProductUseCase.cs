using AutoMapper;
using ProductService.Application.Exceptions;
using ProductService.Application.UseCases.Interfaces;
using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;
using Sahred.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.UseCases
{
    public class CRUDProductUseCase : ICRUDProductUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        #region Ctor
        public CRUDProductUseCase(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        #endregion


        #region Public Functions
        public async Task<bool> AddAsync(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            await _productRepository.AddProductAsync(product);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _productRepository.DeleteProductAsync(id);
            return true;
        }

        public async Task<List<ProductDTO>> GetAllAsync()
        {
            var products = await _productRepository.GetAllProductsAsync() ?? new List<Product>();
            return products.Select(x => _mapper.Map<ProductDTO>(x)).ToList();
        }

        public async Task<bool> UpdateAsync(ProductDTO productDTO)
        {
            var product = _productRepository.GetProductByIdAsync(productDTO.Id);
            if (product is null)
                throw new ProductNotFoundException(productDTO.Id);

            var productToUpdate = _mapper.Map<Product>(productDTO);
            await _productRepository.UpdateProductAsync(productToUpdate);
            return true;
        }

        public async Task<ProductDTO?> GetProductById(Guid id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            return _mapper.Map<ProductDTO?>(product);
        }
        #endregion
    }
}
