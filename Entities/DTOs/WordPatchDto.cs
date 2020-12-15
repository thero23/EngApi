using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DTOs
{
    public class WordPatchDto
    {
        [MaxLength(250)]
        public string Original { get; set; }

        [MaxLength(250)]
        public string Translate { get; set; }
    }
}
