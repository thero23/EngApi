using System;

namespace English.Services.DTOs
{
    public class DictionaryGetDto
    {
        public Guid Id { get; set; }
        public string SecretName { get; set; }
        public string Name { get; set; }
    }
}
