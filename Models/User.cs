using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishApi.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password{ get; set; }

        [Required]
        public Guid UserRoleId { get; set; }

        [ForeignKey("UserRoleId")]
        public UserRole UserRole { get; set; }
    }
}
