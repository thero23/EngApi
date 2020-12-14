using Contracts;
using Entities.Data;
using Entities.Models;

namespace Repository
{
    public class DictionaryRepository:BaseRepository<Dictionary>, IDictionaryRepository
    {
        public DictionaryRepository(EnglishContext context) : base(context)
        {
        }

    }
}
