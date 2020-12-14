using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Entities.Data.Interfaces;
using Entities.Models;
using English.Services.Interfaces;

namespace English.Services
{
    public class WordDictionaryService:IWordDictionaryService
    {
        private readonly IRepositoryManager _repository;
        public WordDictionaryService(IRepositoryManager repository)
        {
            _repository = repository;
        }


        //words
        public IQueryable<Word> FindAllWords(bool trackChanges)
        {
            return _repository.Word.FindAll(trackChanges);
        }

        public IQueryable<Word> FindWordByCondition(Expression<Func<Word, bool>> expression, bool trackChanges)
        {
            return _repository.Word.FindByCondition(expression, trackChanges);
        }

        public async Task CreateWord(Word entity)
        {
            await _repository.Word.Create(entity);
        }

        public void UpdateWord(Word entity)
        {
            _repository.Word.Update(entity);
        }

        public void DeleteWord(Word entity)
        {
            _repository.Word.Delete(entity);
        }

        
        // dictionaries

        public IQueryable<Dictionary> FindAllDictionaries(bool trackChanges)
        {
            return _repository.Dictionary.FindAll(trackChanges);
        }

        public IQueryable<Dictionary> FindDictionaryByCondition(Expression<Func<Dictionary, bool>> expression, bool trackChanges)
        {
            return _repository.Dictionary.FindByCondition(expression, trackChanges);
        }

        public async Task CreateDictionary(Dictionary entity)
        {
            await _repository.Dictionary.Create(entity);
        }

        public void UpdateDictionary(Dictionary entity)
        {
            _repository.Dictionary.Update(entity);
        }

        public void DeleteDictionary(Dictionary entity)
        {
            _repository.Dictionary.Delete(entity);
        }


        //Actions between words and dictionaries

        public async Task<IEnumerable<Word>> GetWordsFromDictionary(Dictionary dictionary)
        {
            return await _repository.Word.GetWordsFromDictionary(dictionary);
        }

        public async Task AddWordToDictionary(Word word, Dictionary dictionary)
        {
            await _repository.Word.AddWordToDictionary(word, dictionary);
        }

        public void RemoveWordFromDictionary(Word word, Dictionary dictionary)
        {
            _repository.Word.RemoveWordFromDictionary(word,dictionary);
        }

        public bool IsWordInDictionary(Word word, Dictionary dictionary)
        {
            return _repository.Word.IsWordInDictionary(word, dictionary);
        }



        public async Task Save() => await _repository.Save();
    }
}
