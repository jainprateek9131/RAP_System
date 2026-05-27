using EnterpriseApp.Application.DTOs;
using EnterpriseApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriceApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // REGISTER

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);

            return Success(
                result,
                201,
                "User Registered Successfully"
            );
        }

        // LOGIN

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);

            if (result == null)
            {
                return UnauthorizedResponse(
                    "Invalid Email or Password"
                );
            }

            return Success(
                result,
                "Login Successful"
            );
        }
    }
}
