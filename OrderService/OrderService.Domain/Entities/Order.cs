using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public long Quantity { get; set; }
        public decimal Total { get; set; }
        public Guid ClientId { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid LoggedInEmployeeId { get; set; }

        public Order() { }

        public Order(Guid id, Guid productId, long quantity, decimal total, Guid clientId, DateTime orderDate, Guid loggedInUser)
        {
            Id = id;
            ProductId = productId;
            Quantity = quantity;
            Total = total;
            ClientId = clientId;
            OrderDate = orderDate;
            LoggedInEmployeeId = loggedInUser;
        }
    }
}
