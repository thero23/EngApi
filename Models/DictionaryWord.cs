using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishApi.Models
{
    public class DictionaryWord
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid DictionaryId { get; set; }

        [ForeignKey("DictionaryId")]
        public Dictionary Dictionary { get; set; }

        [Required]
        public Guid WordId { get; set; }

        [ForeignKey("WordId")]
        public Word Word { get; set; }
    }
}
