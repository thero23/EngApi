using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishApi.Models
{
    public class BaseEntity:IEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
