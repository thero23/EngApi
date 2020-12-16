using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IWordRepository
    {
        Task<IEnumerable<Word>> GetAllWordsAsync(bool trackChanges);
        Task<IEnumerable<Word>> GetWordsByConditionAsync(Expression<Func<Word, bool>> expression, bool trackChanges);
        Task CreateWordAsync(Word word);
        Task<IEnumerable<Word>> GetByIdsAsync(IEnumerable<Guid> ids, bool
            trackChanges);
        void DeleteWord(Word word);
        void UpdateWord(Word word);

        Task<IEnumerable<Word>> GetWordsFromDictionary(Dictionary dictionary);
        Task AddWordToDictionary(Word word, Dictionary dictionary);
        void RemoveWordFromDictionary(Word word, Dictionary dictionary);
        Task<bool> IsWordInDictionary(Word word, Dictionary dictionary);

    }
}
