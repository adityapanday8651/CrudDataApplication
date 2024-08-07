using CrudDataApplication.DataContext;
using CrudDataApplication.Interfaces;
using CrudDataApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CrudDataApplication.Repositories
{
    public class JwtRepository : IJwtRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IBaseRepository<RefreshToken> _repository;

        private readonly AppDbContext _context;
        public JwtRepository(AppDbContext context, IConfiguration configuration, IBaseRepository<RefreshToken> repository)
        {
            _context = context;
            _configuration = configuration;
            _repository = repository;
        }

        protected DbSet<RefreshToken> DbSet() => _context.RefreshTokens;

        public async Task<RefreshToken> GenerateRefreshTokenAsync(string userName)
        {
            RefreshToken refreshToken = new RefreshToken
            {
                Token = GenerateRandomToken(),
                JwtId = Guid.NewGuid().ToString(),
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMinutes(30),
                UserName = userName,
                IsUsed = false,
                IsRevoked = false
            };
            await _repository.AddAsync(refreshToken);
            return refreshToken;
        }

        public string GenerateToken(string userName, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<RefreshToken> ValidateRefreshTokenAsync(string token)
        {
            var refreshToken = DbSet().SingleOrDefault(rt => rt.Token == token);

            if (refreshToken == null || refreshToken.IsUsed || refreshToken.IsRevoked || refreshToken.ExpiryDate < DateTime.UtcNow)
            {
                return null;
            }

            refreshToken.IsUsed = true;
            await _repository.UpdateAsync(refreshToken);

            return refreshToken;
        }


        private string GenerateRandomToken()
        {
            var randomBytes = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }
    }
}
