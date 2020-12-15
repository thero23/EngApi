using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class DictionaryUpdateDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        [MaxLength(250)]
        public string SecretName { get; set; }
    }
}
