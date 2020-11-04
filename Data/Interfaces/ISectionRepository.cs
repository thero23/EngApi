using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishApi.Models;

namespace EnglishApi.Data.Interfaces
{
    interface ISectionRepository:IBaseRepository<Section>
    {
        Task AddUserToSection(User user, Section section);
        Task AddSubsectionToSection(Subsection subsection, Section section);
        bool IsSubsectionInSection(Subsection subsection, Section section);
        Task DeleteUserFromSection(User user, Section section);
        Task<bool> IsHasAccess(User user, Section section);
        Task AddDictionaryToSection(Dictionary dictionary, Section section);
        Task<bool> IsDictionaryInSection(Dictionary dictionary, Section section);




    }
}
