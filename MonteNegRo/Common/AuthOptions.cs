using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MonteNegRo.Common
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
