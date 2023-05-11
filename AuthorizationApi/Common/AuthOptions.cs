using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AuthorizationApi.Common
{
    public class AuthOptions
    {
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public string Secret { get; init; }
        public int TokenLifetime { get; init; } // sec
        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        }
    }
}
