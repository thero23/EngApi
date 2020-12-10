﻿using System;
using System.ComponentModel.DataAnnotations;

namespace English.Database.Models
{
    public class Word
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Original { get; set; }

        [Required]
        [MaxLength(250)]
        public string Translate { get; set; }

       
    }
}