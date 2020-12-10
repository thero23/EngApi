using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using English.Database.Data.Interfaces;
using English.Database.Data.Repositories;
using English.Database.Models;

namespace English.Services.Interfaces
{
    public interface IWordDictionaryService
    {

        //words
        IQueryable<Word> FindAllWords(bool trackChanges);
        IQueryable<Word> FindWordByCondition(Expression<Func<Word, bool>> expression,
            bool trackChanges);
        Task CreateWord(Word entity);
        void UpdateWord(Word entity);
        void DeleteWord(Word entity);





        //dictionaries
        IQueryable<Dictionary> FindAllDictionaries(bool trackChanges);
        IQueryable<Dictionary> FindDictionaryByCondition(Expression<Func<Dictionary, bool>> expression,
            bool trackChanges);
        Task CreateDictionary(Dictionary entity);
        void UpdateDictionary(Dictionary entity);
        void DeleteDictionary(Dictionary entity);


        //WordDictionary
        Task<IEnumerable<Word>> GetWordsFromDictionary(Dictionary dictionary);
        Task AddWordToDictionary(Word word, Dictionary dictionary);
        void RemoveWordFromDictionary(Word word, Dictionary dictionary);
        bool IsWordInDictionary(Word word, Dictionary dictionary);

        public Task Save();
    }
}
