using CrudDataApplication.Models;

namespace CrudDataApplication.Interfaces
{
    public interface IJwtRepository
    {
        string GenerateToken(string userName, string role);
        Task<RefreshToken> GenerateRefreshTokenAsync(string userName);
        Task<RefreshToken> ValidateRefreshTokenAsync(string token);
    }
}
