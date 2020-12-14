using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Word
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Original word is a required field.")]
        [MaxLength(250, ErrorMessage = "Maximum length for the word is 250 characters.")]
        public string Original { get; set; }

        [Required(ErrorMessage = "Translate word is a required field.")]
        [MaxLength(250, ErrorMessage = "Maximum length for the translated word is 250 characters.")]
        public string Translate { get; set; }

       
    }
}
