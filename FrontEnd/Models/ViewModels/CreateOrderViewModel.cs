namespace FrontEnd.Models.ViewModels
{
    public class CreateOrderViewModel
    {
        public OrderDTO Order { get; set; }
        public IEnumerable<ProductDTO> Products { get; set; }
        //public IEnumerable<UserDTO> Clients { get; set; }
    }
}
