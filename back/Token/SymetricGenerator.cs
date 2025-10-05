using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace back.Token
{
    public class SymetricGenerator
    {
        public SymmetricSecurityKey Generator()
        {
            var secretKey = "1234567890ABCDEFGHIJKLMNOPQRSTUV";
            var securityKey = Encoding.UTF8.GetBytes(secretKey);
            return new SymmetricSecurityKey(securityKey);
        }
    }
}