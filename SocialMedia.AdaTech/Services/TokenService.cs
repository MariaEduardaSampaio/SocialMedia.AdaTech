using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.AdaTech.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialMedia.AdaTech.Services
{
    public class TokenService
    {
        private IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken(Credencial credencial)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, credencial.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["SecurityKey"])),
                    SecurityAlgorithms.HmacSha256)
            };

            var chaveToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(chaveToken);
        }
    }
}
