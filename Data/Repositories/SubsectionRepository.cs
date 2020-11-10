using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishApi.Models;

namespace EnglishApi.Data.Repositories
{
    public class SubsectionRepository:BaseRepository<Subsection>, ISubsectionRepository
    {
        public SubsectionRepository(EnglishContext context) : base(context)
        {
        }
    }
}
