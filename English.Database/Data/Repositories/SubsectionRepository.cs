using English.Database.Data.Interfaces;
using English.Database.Models;

namespace English.Database.Data.Repositories
{
    public class SubsectionRepository:BaseRepository<Subsection>, ISubsectionRepository
    {
        public SubsectionRepository(EnglishContext context) : base(context)
        {
        }
    }
}
