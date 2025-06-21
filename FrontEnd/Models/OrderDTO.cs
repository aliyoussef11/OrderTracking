namespace FrontEnd.Models
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public long Quantity { get; set; }
        public decimal Total { get; set; }
        public Guid ClientId { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
