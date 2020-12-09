using System;
using System.ComponentModel.DataAnnotations;

namespace English.Database.Models
{
    public class BaseEntity:IEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
