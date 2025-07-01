using LibraryManagementSystem.DTOs;
using LibraryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto registerDto)
        {
            try
            {
                var user = await _authService.RegisterAsync(registerDto);
                return Ok(new { user.Id, user.Name, user.Username });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            try
            {
                var user = await _authService.LoginAsync(loginDto);
                var token = _authService.GenerateJwtToken(user);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
} 