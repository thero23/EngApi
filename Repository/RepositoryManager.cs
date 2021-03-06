﻿using System.Threading.Tasks;
using Contracts;
using Entities.Data;

namespace Repository
{
    public class RepositoryManager:IRepositoryManager
    {
        private readonly EnglishContext _context;
        private IWordRepository _wordRepository;
        private IDictionaryRepository _dictionaryRepository;
     
        private ISectionRepository _sectionRepository;
        private ISubsectionRepository _subsectionRepository;
        private IUserRepository _userRepository;
    

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

      

        public ISectionRepository Section
        {
            get
            {
                if (_sectionRepository == null)
                    _sectionRepository = new SectionRepository(_context);
                return _sectionRepository;
            }
        }

        public ISubsectionRepository Subsection
        {
            get
            {
                if (_subsectionRepository == null)
                    _subsectionRepository = new SubsectionRepository(_context);
                return _subsectionRepository;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_context);
                return _userRepository;
            }
        }

      
        public async Task Save() => await _context.SaveChangesAsync();
    }
}
