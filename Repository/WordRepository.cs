using System.Collections.Generic;
using System.Linq;
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

        public bool IsWordInDictionary(Word word, Dictionary dictionary)
        {
            return _context.DictionaryWords.Any(p => p.DictionaryId == dictionary.Id && p.WordId == word.Id);



        }


    }
}
