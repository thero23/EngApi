using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishApi.Contracts
{
    public class DeleteWordFromDictionaryRequest
    {
        public Guid DictionaryId { get; set; }
        public Guid WordId { get; set; }

    }
}
