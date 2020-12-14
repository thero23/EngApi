using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
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
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
