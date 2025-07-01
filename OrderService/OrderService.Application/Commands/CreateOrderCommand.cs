﻿using MediatR;

namespace OrderService.Application.Commands
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public long Quantity { get; set; }
        public decimal Total { get; set; }
        public Guid ClientId { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid LoggedInEmployeeId { get; set; }
    }
}
