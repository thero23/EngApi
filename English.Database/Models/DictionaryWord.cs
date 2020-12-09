using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace English.Database.Models
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
