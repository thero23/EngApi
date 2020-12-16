using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts;
using Entities.Data;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class WordRepository:BaseRepository<Word>, IWordRepository
    {
        public WordRepository(EnglishContext context) : base(context)
        {
        }



        public async Task<IEnumerable<Word>> GetAllWordsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(c => c.Original)
                .ToListAsync();

        public async Task<IEnumerable<Word>> GetWordsByConditionAsync(Expression<Func<Word, bool>> expression, bool trackChanges)
        {
            return await FindByCondition(expression, trackChanges).ToListAsync();

        }

      

        public async Task CreateWordAsync(Word company)
        {
            await Create(company);
        }

        public async Task<IEnumerable<Word>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            return await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();
        }

        public void DeleteWord(Word word)
        {
            Delete(word);
        }

        public void UpdateWord(Word word)
        {
            Update(word);
        }
        
        // word with dictionary operations

        public async Task<IEnumerable<Word>> GetWordsFromDictionary(Dictionary dictionary)
        {
            var wordsId = (await _context.DictionaryWords.Where(p => p.DictionaryId == dictionary.Id).ToListAsync()).Select(p => p.WordId).ToList();


            return await _context.Words.Where(p => wordsId.Contains(p.Id)).ToListAsync();
        }

        public async Task AddWordToDictionary(Word word, Dictionary dictionary)
        {

            var item = new DictionaryWord
            {
                WordId = word.Id,
                DictionaryId = dictionary.Id
            };

            await _context.DictionaryWords.AddAsync(item);


        }

        public void RemoveWordFromDictionary(Word word, Dictionary dictionary)
        {
            var item = _context.DictionaryWords.FirstOrDefault(p => p.Dictionary == dictionary && p.Word == word);

            _context.DictionaryWords.Remove(item);


        }

        public async Task<bool> IsWordInDictionary(Word word, Dictionary dictionary)
        {
            return await _context.DictionaryWords.AnyAsync(p => p.DictionaryId == dictionary.Id && p.WordId == word.Id);

        }


    }
}
