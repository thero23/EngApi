using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using EnglishApi.Models;

namespace EnglishApi.DTOs
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
