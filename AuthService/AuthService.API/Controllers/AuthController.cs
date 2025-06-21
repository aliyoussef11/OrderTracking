using AuthService.Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.DTOs;

namespace AuthService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IRegisterUseCase registerUseCase, ILoginUseCase loginUseCase) : ControllerBase
    {
        [HttpPost("register")] 
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            try
            {
                await registerUseCase.RegisterAsync(registerDto);
                return Ok(new { message = "Your account has been created." });
            }
            catch (ApplicationException ex) 
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("login")] 
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            try
            {
                var response = await loginUseCase.LoginAsync(loginDto);
                return Ok(response);
            }
            catch (UnauthorizedAccessException) 
            {
                return Unauthorized(new { error = "Invalid credentials." });
            }
        }
    }
}
