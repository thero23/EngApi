using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Contracts;
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
        public async Task<IEnumerable<Word>> FindAllWords(bool trackChanges)
        {
            return await _repository.Word.GetAllWordsAsync(trackChanges);
        }

        
        public async Task<IEnumerable<Word>> FindWordByCondition(Expression<Func<Word, bool>> expression, bool trackChanges)
        {
            return await _repository.Word.GetWordsByConditionAsync(expression, trackChanges);
        }

        public async Task<IEnumerable<Word>> FindWordsByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            return await _repository.Word.GetByIdsAsync(ids, trackChanges);
        }

        public async Task CreateWord(Word entity)
        {
            await _repository.Word.CreateWordAsync(entity);
        }

        public void UpdateWord(Word entity)
        {
            _repository.Word.UpdateWord(entity);
        }

        public void DeleteWord(Word entity)
        {
            _repository.Word.DeleteWord(entity);
        }

        
        // dictionaries

        public async Task<IEnumerable<Dictionary>> FindAllDictionaries(bool trackChanges)
        {
            return await _repository.Dictionary.GetAllDictionariesAsync(trackChanges);
        }

        public async Task<IEnumerable<Dictionary>> FindDictionariesByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            return await _repository.Dictionary.GetByIdsAsync(ids, trackChanges);
        }

        public async Task<IEnumerable<Dictionary>> FindDictionariesByCondition(Expression<Func<Dictionary, bool>> expression, bool trackChanges)
        {
            return await _repository.Dictionary.GetDictionariesByConditionAsync(expression, trackChanges);
        }

        public async Task CreateDictionary(Dictionary entity)
        {
            await _repository.Dictionary.CreateDictionaryAsync(entity);
        }

        public void UpdateDictionary(Dictionary entity)
        {
            _repository.Dictionary.UpdateDictionary(entity);
        }

        public void DeleteDictionary(Dictionary entity)
        {
            _repository.Dictionary.DeleteDictionary(entity);
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

        public async Task<bool> IsWordInDictionary(Word word, Dictionary dictionary)
        {
            return await _repository.Word.IsWordInDictionary(word, dictionary);
        }



        public async Task Save() => await _repository.Save();
    }
}
