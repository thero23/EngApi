using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Dictionary
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string SecretName { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
    }
}
