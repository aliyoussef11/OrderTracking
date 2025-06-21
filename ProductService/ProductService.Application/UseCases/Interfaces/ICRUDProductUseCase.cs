using Sahred.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.UseCases.Interfaces
{
    public interface ICRUDProductUseCase
    {
        Task<List<ProductDTO>> GetAllAsync();
        Task<bool> AddAsync(ProductDTO productDTO);
        Task<bool> UpdateAsync(ProductDTO productDTO);
        Task<bool> DeleteAsync(Guid id);
        Task<ProductDTO?> GetProductById(Guid id);
    }
}
