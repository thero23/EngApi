using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishApi.Models;

namespace EnglishApi.DTOs
{
    public class SubsectionGetDto:BaseEntity
    {

        public string Name { get; set; }

        public int Order { get; set; }

        public Guid? SectionId { get; set; }

    }
}
