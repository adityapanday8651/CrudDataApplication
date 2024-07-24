using CrudDataApplication.Interfaces;
using CrudDataApplication.Models;

namespace CrudDataApplication.Services
{
    public class JwtService : IJwtService
    {
        private readonly IJwtRepository _jwtRepository;

        public JwtService(IJwtRepository jwtRepository)
        {
            _jwtRepository = jwtRepository;
        }
        public Task<RefreshToken> GenerateRefreshTokenAsync(string userName)
        {
            return _jwtRepository.GenerateRefreshTokenAsync(userName);
        }

        public string GenerateToken(string userName, string role)
        {
            return _jwtRepository.GenerateToken(userName, role);
        }

        public async Task<RefreshToken> ValidateRefreshTokenAsync(string token)
        {
            return await _jwtRepository.ValidateRefreshTokenAsync(token);
        }
    }
}
