using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DTOs
{
    public abstract class WordForManipulationDto
    {
        [Required(ErrorMessage = "Original is a required field.")]
        [MaxLength(250, ErrorMessage = "Maximum length for field Original is 250 characters.")]
        public string Original { get; set; }

        [Required(ErrorMessage = "Translate is a required field.")]
        [MaxLength(250, ErrorMessage = "Maximum length for field Translate is 250 characters.")]
        public string Translate { get; set; }
    }
}
