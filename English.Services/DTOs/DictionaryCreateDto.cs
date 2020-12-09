using System.ComponentModel.DataAnnotations;

namespace English.Services.DTOs
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
