using Shared.Common.DTOs;
using System;

namespace AuthService.Application.UseCases.Interfaces
{
    public interface ILoginUseCase
    {
        Task<LoginResponseDTO> LoginAsync(LoginDTO loginPayload);
    }
}
