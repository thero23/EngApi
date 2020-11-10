using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishApi.DTOs
{
    public class DictionaryCreateDto
    {
        [Required]
        [MaxLength(250)]
        public string SecretName { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
    }
}
