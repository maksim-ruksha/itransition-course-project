using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CourseProject.Auth
{
    // ауф 2
    public static class JwtAuthOptions
    {
        public const string Issuer = "Issuer";
        public const string Audience = "Audience";
        public const string Key = "1234567890qwertyuiopasdfghjkl";
        public const int Lifetime = 50000; 

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}