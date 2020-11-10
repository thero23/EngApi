using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishApi.DTOs
{
    public class WordGetDto
    {
        public Guid Id { get; set; }
        public string Original { get; set; }
        public string Translate { get; set; }
    }
}
