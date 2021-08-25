using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class UserRequestModels
    {
        public int Id { get; set; }
        public string Login { get; set; }
        [Required, StringLength(150, MinimumLength = 3)]
        public string Name { get; set; }
        [Required, EmailAddress, StringLength(150, MinimumLength = 3)]
        public string Email { get; set; }
    }
}
