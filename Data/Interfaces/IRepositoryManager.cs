using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishApi.Data.Interfaces
{
    public interface IRepositoryManager
    {
        IDictionaryRepository Dictionary { get; }
        IWordRepository Word { get; }
        IDictionaryWordRepository DictionaryWord { get; }
        Task Save();
    }
}
