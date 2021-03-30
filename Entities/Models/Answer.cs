using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Answer
    {
        [Key] public Guid Id { get; set; }

        [Required(ErrorMessage = "Text is a required field.")]
        [MaxLength(250, ErrorMessage = "Maximum length for the Text is 250 characters.")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Order is a required field.")]
        public int Order { get; set; }


    }
}
