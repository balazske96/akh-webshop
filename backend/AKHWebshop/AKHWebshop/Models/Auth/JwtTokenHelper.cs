using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AKHWebshop.Models.Auth
{
    public class JwtTokenHelper
    {
        private JwtTokenHelperOptions Options { get; set; }

        public JwtTokenHelper(JwtTokenHelperOptions options)
        {
            Options = options;
        }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Options.SecretKey));
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = Options.Issuer,
                Audience = Options.Audience,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}