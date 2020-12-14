using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Dictionary
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Secret name is a required field.")]
        [MaxLength(250, ErrorMessage = "Maximum length for secret name is 250 characters.")]
        public string SecretName { get; set; }

        [Required(ErrorMessage = "Name is a required field.")]
        [MaxLength(250, ErrorMessage = "Maximum length for name is 250 characters.")]
        public string Name { get; set; }
    }
}
