using System.Text;
using System.Security.Cryptography;
using System;
using System.Security.Claims;
using FoodDelivery_Backend.Models.ViewModel;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace FoodDelivery_Backend.Services {
    public class TokenService : ITokenService {
        private const double EXPIRATION_TIME_DAYS = 1;
        public string BuildToken(string key, string issuer, VMUser user) {
            var claims = new[] {
                new Claim(ClaimTypes.Name, user.Fullname),
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims, expires: DateTime.Now.AddDays(EXPIRATION_TIME_DAYS), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public bool IsValidToken(string key, string issuer, string audience, string token) {
            var mySecret = Encoding.UTF8.GetBytes(key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);
            var tokenHandler = new JwtSecurityTokenHandler();

            try {
                tokenHandler.ValidateToken(token, new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = issuer,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);
            } catch {
                return false;
            }
            return true;
        }
    }
}