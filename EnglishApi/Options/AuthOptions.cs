using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishApi.Options
{
    public class AuthOptions
    {
        public const string ISSUER = "EnglishApi"; // издатель токена
        public const string AUDIENCE = "EnglishApi"; // потребитель токена
        const string KEY = "bf95c7195de04b669bc89854338f4098";   // ключ для шифрации
        public const int LIFETIME = 5; // время жизни токена - 60 минут
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
