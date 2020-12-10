﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace English.Database.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password{ get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }
        [Required]
        public Guid UserRoleId { get; set; }

        [ForeignKey("UserRoleId")]
        public UserRole UserRole { get; set; }
    }
}