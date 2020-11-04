using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishApi.Models
{
    public class SectionUser
    {
        [Key]
        public Guid Id{ get; set; }

        [Required]
        public Guid SectionId { get; set; }
        
        [ForeignKey("SectionId")]
        public Section Section { get; set; }

        [Required]
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
