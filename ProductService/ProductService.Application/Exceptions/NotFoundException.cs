using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Exceptions
{
    public class ProductNotFoundException : ApplicationException
    {
        public ProductNotFoundException(Guid id)
            : base($"Product {id} is not found.")
        {
        }
    }
}
