using Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DataContext.Entity
{
    public abstract class EntityModel : IBaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}
