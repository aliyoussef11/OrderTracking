using AuthService.Application.Exceptions;
using AuthService.Application.Helpers;
using AuthService.Application.UseCases.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using Shared.Common.DTOs;

namespace AuthService.Application.UseCases
{
    public class RegisterUseCase : IRegisterUseCase
    {
        private readonly IUserRepository _userRepository;

        #region ctor
        public RegisterUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        #region Public Functions
        public async Task RegisterAsync(RegisterDTO registerPayload)
        {
            ValidateEmail(registerPayload.Email);
            string hashedPassword = HashingHelper.HashPasswordWithSHA256(registerPayload.Password);

            var userToAdd = new User(registerPayload.Email, hashedPassword);
            await _userRepository.AddAsync(userToAdd);
        }
        #endregion

        #region Private Functions
        private async void ValidateEmail(string email)
        {
            if (await _userRepository.GetAsync(email) is not null)
                throw new EmailAlreadyRegisteredException(email);
        }
        #endregion
    }
}
