using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishApi.Data.Interfaces;

namespace EnglishApi.Data.Repositories
{
    public class RepositoryManager:IRepositoryManager
    {
        private readonly EnglishContext _context;
        private IWordRepository _wordRepository;
        private IDictionaryRepository _dictionaryRepository;
        private IDictionaryWordRepository _dictionaryWordRepository;

        public RepositoryManager(EnglishContext context)
        {
            _context = context;
        }

        public IDictionaryRepository Dictionary
        {
            get
            {
                if (_dictionaryRepository == null)
                    _dictionaryRepository = new DictionaryRepository(_context);
                return _dictionaryRepository;
            }
        }

        public IWordRepository Word
        {
            get
            {
                if (_wordRepository == null)
                    _wordRepository = new WordRepository(_context);
                return _wordRepository;
            }
        }

        public IDictionaryWordRepository DictionaryWord
        {
            get
            {
                if (_dictionaryWordRepository == null)
                    _dictionaryWordRepository = new DictionaryWordRepository(_context);
                return _dictionaryWordRepository;
            }
        }

        public void Save() => _context.SaveChanges();
    }
}
