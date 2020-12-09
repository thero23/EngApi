using System.Collections.Generic;
using System.Threading.Tasks;
using English.Database.Models;

namespace English.Database.Data.Interfaces
{
    public interface IWordRepository:IBaseRepository<Word>
    {
        Task<IEnumerable<Word>> GetWordsFromDictionary(Dictionary dictionary);
        Task AddWordToDictionary(Word word, Dictionary dictionary);
        void RemoveWordFromDictionary(Word word, Dictionary dictionary);
        bool IsWordInDictionary(Word word, Dictionary dictionary);
    }
}
