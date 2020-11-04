using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishApi.Models
{
    public class SectionDictionary
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid SectionId { get; set; }

        [ForeignKey("SectionId")]
        public Section Section { get; set; }

        [Required]
        public Guid DictionaryId { get; set; }
        
        [ForeignKey("DictionaryId")]
        public Dictionary Dictionary { get; set; }
    }
}
