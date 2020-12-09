using System;
using System.ComponentModel.DataAnnotations;

namespace English.Database.Models
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
