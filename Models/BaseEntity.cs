using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishApi.Models
{
    public class BaseEntity:IEntity
    {
        public Guid Id { get; set; }
    }
}
