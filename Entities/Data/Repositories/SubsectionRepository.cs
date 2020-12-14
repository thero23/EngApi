using Entities.Data.Interfaces;
using Entities.Models;

namespace Entities.Data.Repositories
{
    public class SubsectionRepository:BaseRepository<Subsection>, ISubsectionRepository
    {
        public SubsectionRepository(EnglishContext context) : base(context)
        {
        }
    }
}
