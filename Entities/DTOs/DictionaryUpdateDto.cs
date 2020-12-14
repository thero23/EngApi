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
        public string Original { get; set; }

        [Required]
        [MaxLength(250)]
        public string Translate { get; set; }
    }
}
