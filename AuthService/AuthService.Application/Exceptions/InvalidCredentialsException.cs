﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Exceptions
{
    public class InvalidCredentialsException : UnauthorizedAccessException
    {
        public InvalidCredentialsException()
            : base("Invalid email or password.")
        {
        }
    }
}
