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
        public const string AUDIENCE = "EnglishClient"; // потребитель токена
        const string KEY = "EnglishTokenKey";   // ключ для шифрации
        public const int LIFETIME = 60; // время жизни токена - 60 минут
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
