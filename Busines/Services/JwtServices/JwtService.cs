using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Qyrenx.Dataccess.ApplicationDbContext;
using Qyrenx.Dataccess.DbAccess;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Qyrenx.Business.Services.JwtServices
{
    public class JwtService:IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly QyrenxContext _context;


        public JwtService(IConfiguration configuration,QyrenxContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        public string GenerateJwt(Guid Id, string Email, string role)
        {
            var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
            new Claim(ClaimTypes.Email, Email),
            new Claim(ClaimTypes.Role, role)
        };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        public async Task<string> CreaterefreshToken(Guid Id, string Email, string role)
        {

            var claims = new[]
         {
            new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.Integer64), 
            new Claim(JwtRegisteredClaimNames.Exp, DateTime.UtcNow.AddDays(30).ToString(), ClaimValueTypes.Integer64),
            new Claim(ClaimTypes.Email, Email),
            new Claim(ClaimTypes.Role, role)
        };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:RefreshKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
