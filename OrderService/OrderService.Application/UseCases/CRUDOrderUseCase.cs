using AutoMapper;
using OrderService.Application.Exceptions;
using OrderService.Application.UseCases.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;
using Shared.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.UseCases
{
    public class CRUDOrderUseCase : ICRUDOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        #region Ctor
        public CRUDOrderUseCase(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        #endregion

        #region Public Functions
        public async Task<bool> AddAsync(OrderDTO orderDTO)
        {
            var order = _mapper.Map<Order>(orderDTO);
            await _orderRepository.AddAsync(order);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _orderRepository.DeleteAsync(id);
            return true;
        }

        public async Task<List<OrderDTO>> GetAllOrdersByEmployeeIdAsync(Guid employeeId)
        {
            var orders = await _orderRepository.GetAllByEmployeeIdAsync(employeeId) ?? new List<Order>();
            return orders.Select(x => _mapper.Map<OrderDTO>(x)).ToList();
        }

        public async Task<bool> UpdateAsync(OrderDTO orderDTO)
        {
            var order = _orderRepository.GetOrderByIdAsync(orderDTO.Id);
            if (order is null)
                throw new OrderNotFoundException(orderDTO.Id);

            var orderToUpdate = _mapper.Map<Order>(orderDTO);
            await _orderRepository.UpdateAsync(orderToUpdate);
            return true;
        }

        public async Task<OrderDTO?> GetOrderById(Guid id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            return _mapper.Map<OrderDTO?>(order);
        }
        #endregion
    }
}
