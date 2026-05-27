using EnterpriceApp.API.Common;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriceApp.API.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        // ✅ Success Response (200 OK)
        protected IActionResult Success<T>(T data, string message = "Success")
        {
            return Ok(new ApiResponse<T>
            {
                StatusCode = 200,
                Success = true,
                Message = message,
                Data = data,
                Errors = null
            });
        }

        // ✅ Custom Status Success (201, 202 etc.)
        protected IActionResult Success<T>(T data, int statusCode, string message = "Success")
        {
            return StatusCode(statusCode, new ApiResponse<T>
            {
                StatusCode = statusCode,
                Success = true,
                Message = message,
                Data = data,
                Errors = null
            });
        }

        // ❌ Bad Request (400)
        protected IActionResult BadRequestResponse(string message = "Bad Request", List<string>? errors = null)
        {
            return BadRequest(new ApiResponse<object>
            {
                StatusCode = 400,
                Success = false,
                Message = message,
                Data = null,
                Errors = errors
            });
        }

        // ❌ Not Found (404)
        protected IActionResult NotFoundResponse(string message = "Resource not found")
        {
            return NotFound(new ApiResponse<object>
            {
                StatusCode = 404,
                Success = false,
                Message = message,
                Data = null,
                Errors = null
            });
        }

        // ❌ Internal Server Error (500)
        protected IActionResult InternalError(string message = "Internal Server Error")
        {
            return StatusCode(500, new ApiResponse<object>
            {
                StatusCode = 500,
                Success = false,
                Message = message,
                Data = null,
                Errors = null
            });
        }

        // ❌ Unauthorized (401)
        protected IActionResult UnauthorizedResponse(
            string message = "Unauthorized")
        {
            return Unauthorized(new ApiResponse<object>
            {
                StatusCode = 401,
                Success = false,
                Message = message,
                Data = null,
                Errors = null
            });
        }
    }
}
