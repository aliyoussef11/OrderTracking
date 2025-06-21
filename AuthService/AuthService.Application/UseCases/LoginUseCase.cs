using AuthService.Application.Exceptions;
using AuthService.Application.Helpers;
using AuthService.Application.UseCases.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Common.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Application.UseCases
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        #region Ctor
        public LoginUseCase(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }
        #endregion

        #region Public Functions
        public async Task<LoginResponseDTO> LoginAsync(LoginDTO loginPayload)
        {
            var user = await ValidateEmail(loginPayload.Email);
            if (!HashingHelper.VerifyPassword(loginPayload.Password, user.HashedPassword))
                throw new InvalidCredentialsException();

            var jwtToken = GenerateJwtToken(user);

            return new LoginResponseDTO
            {
                Token = jwtToken
            };
        }
        #endregion

        #region Private Functions
        private async Task<User> ValidateEmail(string email)
        {
            var user = await _userRepository.GetAsync(email);
            if (user is null)
                throw new InvalidCredentialsException();

            return user;
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expiryMinsText = jwtSettings["AccessTokenExpiryMinutes"];

            if (string.IsNullOrWhiteSpace(secretKey) || string.IsNullOrWhiteSpace(issuer)
                || string.IsNullOrWhiteSpace(audience) || !int.TryParse(expiryMinsText, out var expiryMins))
            {
                throw new InvalidOperationException("JWT configuration is invalid or incomplete.");
            }

            var claims = new List<Claim>
            {
                // Store Id & Username
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(expiryMins),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}
