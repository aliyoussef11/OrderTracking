using MediatR;
using Shared.Common.DTOs;

namespace OrderService.Application.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderDTO>
    {
        public Guid OrderId { get; set; }

        public GetOrderByIdQuery(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
