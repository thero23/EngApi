using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishApi.DTOs
{
    public class DictionaryGetDto
    {
        public Guid Id { get; set; }
        public string SecretName { get; set; }
        public string Name { get; set; }
    }
}
