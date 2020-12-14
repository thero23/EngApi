using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Subsection:BaseEntity
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        public int Order{ get; set; }

        public string Lecture{ get; set; }

        public Guid? SectionId { get; set; }

        [ForeignKey("SectionId")]
        public Section Section { get; set; }
    }
}
