using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace English.Services.Interfaces
{
    public interface IWordDictionaryService
    {

        //words
        Task<IEnumerable<Word>> FindAllWords(bool trackChanges);
        Task<IEnumerable<Word>> FindWordByCondition(Expression<Func<Word, bool>> expression,
            bool trackChanges);

        Task<IEnumerable<Word>> FindWordsByIds(IEnumerable<Guid> ids, bool trackChanges);
        Task CreateWord(Word entity);
        void UpdateWord(Word entity);
        void DeleteWord(Word entity);





        //dictionaries
        Task<IEnumerable<Dictionary>> FindAllDictionaries(bool trackChanges);
        Task<IEnumerable<Dictionary>> FindDictionariesByCondition(Expression<Func<Dictionary, bool>> expression,
            bool trackChanges);

        Task<IEnumerable<Dictionary>> FindDictionariesByIds(IEnumerable<Guid> ids, bool trackChanges);
        Task CreateDictionary(Dictionary entity);
        void UpdateDictionary(Dictionary entity);
        void DeleteDictionary(Dictionary entity);


        //WordDictionary
        Task<IEnumerable<Word>> GetWordsFromDictionary(Dictionary dictionary);
        Task AddWordToDictionary(Word word, Dictionary dictionary);
        void RemoveWordFromDictionary(Word word, Dictionary dictionary);
        Task<bool> IsWordInDictionary(Word word, Dictionary dictionary);

        public Task Save();
    }
}
