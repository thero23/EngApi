using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class WordUpdateDto:WordForManipulationDto
    {
        [Required(ErrorMessage = "Id is required for update")]
        public Guid Id { get; set; }

    }
}
