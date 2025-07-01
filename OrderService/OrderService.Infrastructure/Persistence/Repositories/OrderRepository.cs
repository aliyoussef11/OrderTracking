using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;
using OrderService.Infrastructure.Persistence.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _orderDbContext;

        #region Ctor
        public OrderRepository(OrderDbContext context)
        {
            _orderDbContext = context;
        }
        #endregion

        #region Public Functions
        public async Task AddAsync(Order order, CancellationToken cancellationToken)
        {
            await _orderDbContext.Orders.AddAsync(order);
            await _orderDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await _orderDbContext.Orders.FindAsync(id);
            if (order is not null)
            {
                _orderDbContext.Orders.Remove(order);
                await _orderDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Order>> GetAllByEmployeeIdAsync(Guid employeeId)
        {
            return await _orderDbContext.Orders.AsNoTracking().Where(x => x.LoggedInEmployeeId == employeeId).ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _orderDbContext.Orders.FirstOrDefaultAsync(ord => ord.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(Order order)
        {
            _orderDbContext.Orders.Update(order);
            await _orderDbContext.SaveChangesAsync();
        }
        #endregion
    }
}
