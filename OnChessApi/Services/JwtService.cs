using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

using OnChessApi.Models;

namespace OnChessApi.Services
{
    public class JwtService
    {
        private readonly UserModel? _user = null;

        public JwtService() 
        {
        }

        public JwtService(UserModel user) 
            : this()
        {
            _user = user;
        }

        public string GetToken()
        {
            List<Claim> claims = new List<Claim>
            {
                new ("ID", _user.UserID.ToString()),
                new ("Name", _user.FirstName),
                new ("Email", _user.Email)
            };

            ClaimsIdentity identity = new(claims, "Token");

            if (identity == null)
            {
                return string.Empty;
            }

            DateTime now = DateTime.UtcNow;

            JwtSecurityToken jwt = new (
                issuer: AuthOptionsModel.ISSUER,
                audience: AuthOptionsModel.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptionsModel.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptionsModel.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
