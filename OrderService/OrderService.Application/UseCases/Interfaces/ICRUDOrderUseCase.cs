using Shared.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.UseCases.Interfaces
{
    public interface ICRUDOrderUseCase
    {
        Task<bool> AddAsync(OrderDTO orderDTO);
        Task<bool> UpdateAsync(OrderDTO orderDTO);
        Task<bool> DeleteAsync(Guid id);
        Task<OrderDTO?> GetOrderById(Guid id);
        Task<List<OrderDTO>> GetAllOrdersByEmployeeIdAsync(Guid employeeId);
    }
}
