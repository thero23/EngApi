using Contracts;
using Entities.Data;
using Entities.Models;

namespace Repository
{
    public class SubsectionRepository:BaseRepository<Subsection>, ISubsectionRepository
    {
        public SubsectionRepository(EnglishContext context) : base(context)
        {
        }
    }
}
