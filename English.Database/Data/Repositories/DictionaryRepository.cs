using English.Database.Data.Interfaces;
using English.Database.Models;

namespace English.Database.Data.Repositories
{
    public class DictionaryRepository:BaseRepository<Dictionary>, IDictionaryRepository
    {
        public DictionaryRepository(EnglishContext context) : base(context)
        {
        }

    }
}
