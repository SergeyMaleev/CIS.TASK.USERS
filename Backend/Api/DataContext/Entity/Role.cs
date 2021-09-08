using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DataContext.Entity
{
    public class Role : BaseEntity
    {
            
        /// <summary>
        /// Наименование роли
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Коллекция пользователей, относящихся к этой роли
        /// </summary>
        public ICollection<User> Users { get; set; }
    }
}
