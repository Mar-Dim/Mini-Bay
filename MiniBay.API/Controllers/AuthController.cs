using Microsoft.AspNetCore.Mvc;
using MiniBay.Application.DTOs;
using MiniBay.Application.Exceptions;
using MiniBay.Application.Interfaces;

namespace MiniBay.API.Controllers
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
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            try
            {
                var token = await _authService.RegisterAsync(dto);
                return Ok(new { Token = token });
            }
            catch (UserAlreadyExistsException ex)
            {
                return Conflict(ex.Message); // 409 Conflict es más semántico para un recurso que ya existe
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                var token = await _authService.LoginAsync(dto);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message); // 401 Unauthorized
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}