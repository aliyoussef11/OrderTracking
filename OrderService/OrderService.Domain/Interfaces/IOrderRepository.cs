using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Interfaces
{
    public interface IOrderRepository
    {            
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Guid id);
        Task<Order?> GetOrderByIdAsync(Guid id);
        Task<List<Order>> GetAllByEmployeeIdAsync(Guid employeeId);
    }
}
