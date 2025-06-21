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

        // Logged In User
        public Guid LoggedInEmployeeId { get; set; }
    }
}
