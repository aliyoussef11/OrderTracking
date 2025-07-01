using MediatR;
using OrderService.Domain.Interfaces;
using Shared.Common.DTOs;

namespace OrderService.Application.Queries.Handlers
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDTO>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDTO> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIdAsync(request.OrderId, cancellationToken);
            if (order is null)
                return new OrderDTO();

            return new OrderDTO
            {
                Id = order.Id,
                ProductId = order.ProductId,
                Total = order.Total,
                ClientId = order.ClientId,
                LoggedInEmployeeId = order.LoggedInEmployeeId,
                OrderDate = order.OrderDate,
                Quantity = order.Quantity
            };
        }
    }
}
