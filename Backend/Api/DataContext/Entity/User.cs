using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DataContext.Entity
{
    public class User : BaseEntity
    {
             
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; } 
        
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Хеш пароля
        /// </summary>
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// Уникальная соль к каждому хешу
        /// </summary>
        public byte[] PasswordSalt { get; set; }

        /// <summary>
        /// Коллекция ролей пользователя
        /// </summary>
        public ICollection<Role> Roles { get; set; } = new Collection<Role>();
   
    }

}
