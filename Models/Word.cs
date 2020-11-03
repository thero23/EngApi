using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishApi.Models
{
    public class Word
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Original { get; set; }

        [Required]
        [MaxLength(250)]
        public string Translate { get; set; }
    }
}
