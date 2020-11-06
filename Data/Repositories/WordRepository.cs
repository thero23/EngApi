using System.Collections.Generic;
using EnglishApi.Data.Interfaces;
using EnglishApi.Models;

namespace EnglishApi.Data.Repositories
{
    public class WordRepository:BaseRepository<Word>, IWordRepository
    {
        public WordRepository(EnglishContext context) : base(context)
        {
        }

       

       
    }
}
