using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace English.Database.Models
{
    public class Section:BaseEntity
    {
        [Required]
        [MaxLength(250)]
        public string Name { get;set; }

        [Required]
        public int Order { get; set; }

        public IEnumerable<Subsection> Subsections { get; set; }
    }
}
