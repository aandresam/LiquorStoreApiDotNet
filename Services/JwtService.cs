using LiquorStoreApi.Context.Entities;
using LiquorStoreApi.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LiquorStoreApi.Services
{
    public class JwtService
    {
        private readonly IConfiguration config;

        public JwtService(IConfiguration configuration)
        {
            this.config = configuration;
        }

        public String GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            var apiKey = config.GetSection("Jwt:Key").Value ?? throw new ApiExceptions("No se ha establecido una clave JWT");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.Now.AddHours(10),
                signingCredentials: creds
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
    }
}
