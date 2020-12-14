using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class SubsectionCreateDto
    {

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        public int Order { get; set; }

        public Guid? SectionId { get; set; }

    }
}
