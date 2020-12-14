using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class WordCreateDto
    {

        [Required]
        [MaxLength(250)]
        public string Original { get; set; }

        [Required]
        [MaxLength(250)]
        public string Translate { get; set; }
    }
}
