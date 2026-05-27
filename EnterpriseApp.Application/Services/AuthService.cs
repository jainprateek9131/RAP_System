using EnterpriseApp.Application.DTOs;
using EnterpriseApp.Domain.Entities;
using EnterpriseApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public AuthService(
            IUserRepository userRepository,
            IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<string> RegisterAsync(RegisterRequest request)
        {
            var existingUser = await _userRepository
                .GetUserByEmailAsync(request.Email);

            if (existingUser != null)
            {
                return "User already exists";
            }

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = request.Role
            };

            await _userRepository.AddUserAsync(user);

            return "User Registered Successfully";
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository
                .GetUserByEmailAsync(request.Email);

            if (user == null)
            {
                return null;
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.PasswordHash
            );

            if (!isPasswordValid)
            {
                return null;
            }

            var token = _jwtService.GenerateToken(user);

            return new AuthResponse
            {
                Token = token,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}
