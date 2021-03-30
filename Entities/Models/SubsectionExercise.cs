using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class SubsectionExercise
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid SubsectionId { get; set; }

        [ForeignKey("SubsectionId")]
        public Subsection Subsection { get; set; }

        [Required]
        public Guid ExerciseId { get; set; }

        [ForeignKey("ExerciseId")]
        public Exercise Exercise { get; set; }
    }
}
