using AutoMapper;
using MediatR;
using OrderService.Application.Exceptions;
using OrderService.Application.Queries;
using OrderService.Application.UseCases.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;
using Shared.Common.DTOs;

namespace OrderService.Application.UseCases
{
    public class CRUDOrderUseCase : ICRUDOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #region Ctor
        public CRUDOrderUseCase(IOrderRepository orderRepository, IMapper mapper, IMediator mediator)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _mediator = mediator;
        }
        #endregion

        #region Public Functions
        public async Task<bool> AddAsync(OrderDTO orderDTO)
        {
            //var order = _mapper.Map<Order>(orderDTO);
            //await _orderRepository.AddAsync(order);
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
            //var order = _orderRepository.GetOrderByIdAsync(orderDTO.Id);
            var order = _mediator.Send(new GetOrderByIdQuery(orderDTO.Id));
            if (order is null)
                throw new OrderNotFoundException(orderDTO.Id);

            var orderToUpdate = _mapper.Map<Order>(orderDTO);
            await _orderRepository.UpdateAsync(orderToUpdate);
            return true;
        }

        public async Task<OrderDTO?> GetOrderById(Guid id)
        {
            //var order = await _orderRepository.GetOrderByIdAsync(id);
            var order = _mediator.Send(new GetOrderByIdQuery(id));
            return _mapper.Map<OrderDTO?>(order);
        }
        #endregion
    }
}
