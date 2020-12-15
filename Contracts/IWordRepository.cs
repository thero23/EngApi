using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IWordRepository:IBaseRepository<Word>
    {
        Task<IEnumerable<Word>> FindByIds(IEnumerable<Guid> ids, bool trackChanges);
        Task<IEnumerable<Word>> GetWordsFromDictionary(Dictionary dictionary);
        Task AddWordToDictionary(Word word, Dictionary dictionary);
        void RemoveWordFromDictionary(Word word, Dictionary dictionary);
        bool IsWordInDictionary(Word word, Dictionary dictionary);

    }
}
