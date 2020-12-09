using System;

namespace English.Services.DTOs
{
    public class WordGetDto
    {
        public Guid Id { get; set; }
        public string Original { get; set; }
        public string Translate { get; set; }
    }
}
