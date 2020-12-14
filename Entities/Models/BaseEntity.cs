using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class BaseEntity:IEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
