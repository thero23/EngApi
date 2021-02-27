using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Models
{
    public class Exercise
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Text is a required field.")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Title is a required field.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Answers is a required field.")]
        public List<Answer>  Answers { get; set; }

    }
}
