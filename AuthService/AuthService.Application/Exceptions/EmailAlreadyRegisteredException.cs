using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Exceptions
{
    public class EmailAlreadyRegisteredException : ApplicationException
    {
        public EmailAlreadyRegisteredException(string email)
        : base($"The email '{email}' is already registered.")
        {
        }
    }
}
