using Shared.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.UseCases.Interfaces
{
    public interface IRegisterUseCase
    {
        Task RegisterAsync(RegisterDTO registerPayload);
    }
}
