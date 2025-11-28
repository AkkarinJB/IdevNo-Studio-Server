using IdevNoStudio.Api.Models;
using IdevNoStudio.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdevNoStudio.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;
    private readonly IWebHostEnvironment _environment;

    public AuthController(IAuthService authService, ILogger<AuthController> logger, IWebHostEnvironment environment)
    {
        _authService = authService;
        _logger = logger;
        _environment = environment;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _authService.RegisterAsync(request);

            if (result == null)
            {
                return Conflict(new { message = "Email already exists" });
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during registration: {Message}", ex.Message);
            _logger.LogError("Stack trace: {StackTrace}", ex.StackTrace);
            
            var errorResponse = new Dictionary<string, object>
            {
                { "message", "An error occurred during registration" }
            };
            
            if (_environment.IsDevelopment())
            {
                errorResponse.Add("error", ex.Message);
                errorResponse.Add("innerException", ex.InnerException?.Message ?? "None");
                if (ex.InnerException != null)
                {
                    errorResponse.Add("innerStackTrace", ex.InnerException.StackTrace ?? "None");
                }
            }
            
            return StatusCode(500, errorResponse);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _authService.LoginAsync(request);

            if (result == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login: {Message}", ex.Message);
            _logger.LogError("Stack trace: {StackTrace}", ex.StackTrace);
            
            var errorResponse = new Dictionary<string, object>
            {
                { "message", "An error occurred during login" }
            };
            
            if (_environment.IsDevelopment())
            {
                errorResponse.Add("error", ex.Message);
                errorResponse.Add("innerException", ex.InnerException?.Message ?? "None");
                if (ex.InnerException != null)
                {
                    errorResponse.Add("innerStackTrace", ex.InnerException.StackTrace ?? "None");
                }
            }
            
            return StatusCode(500, errorResponse);
        }
    }
}

