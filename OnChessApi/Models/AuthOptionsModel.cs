using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OnChessApi.Models
{
    public class AuthOptionsModel
    {
        public const string ISSUER = "OnchessServer";

        public const string AUDIENCE = "OnChessClient";

        public const int LIFETIME = 15;

        const string KEY = "mysupersecret_secretkey!123mysupersecret_secretkey!123mysupersecret_secretkey!123";

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
