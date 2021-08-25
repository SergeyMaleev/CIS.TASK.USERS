using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Common
{
    public class AuthOptions
    {
        /// <summary>
        /// Сгенерировавший токен
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Для кого предназначается токен
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Ключ семитричного шифрования
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Длительность жизни токена в секундах
        /// </summary>
        public int TokenLiveTime { get; set; }

        /// <summary>
        /// Возвращает созданый ключ семитричного шифрования 
        /// </summary>
        /// <returns></returns>
        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        }
    }
} 
