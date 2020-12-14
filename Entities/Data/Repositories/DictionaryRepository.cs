using Entities.Data.Interfaces;
using Entities.Models;

namespace Entities.Data.Repositories
{
    public class DictionaryRepository:BaseRepository<Dictionary>, IDictionaryRepository
    {
        public DictionaryRepository(EnglishContext context) : base(context)
        {
        }

    }
}
