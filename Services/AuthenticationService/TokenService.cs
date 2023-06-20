using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace SOAProject.Services.AuthenticationService
{
    public class TokenService
    {
        private const int ExpirationMinutes = 30;

        public string CreateToken(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var expiration = TimeSpan.FromHours(1);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = "apiWithAuthBackend",
                Issuer = "apiWithAuthBackend",
                Subject = new ClaimsIdentity(CreateClaims(user)),
                Expires = DateTime.UtcNow.Add(expiration),
                SigningCredentials = CreateSigningCredentials(),
            };
            var token = tokenHandler.CreateToken(
                tokenDescriptor
            );
            return tokenHandler.WriteToken(token);
        }

        private List<Claim> CreateClaims(IdentityUser user)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new(JwtRegisteredClaimNames.Sub, user.Email),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                    new(ClaimTypes.NameIdentifier, user.Id),
                    new(ClaimTypes.Name, user.UserName),
                    new(ClaimTypes.Email, user.Email),
                    new(ClaimTypes.Role, "Admin")
                };
                return claims;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private SigningCredentials CreateSigningCredentials()
        {
            return new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("MeritonAdemiAdemiMeriton")
                ),
                SecurityAlgorithms.HmacSha256Signature
            );
        }
    }
}