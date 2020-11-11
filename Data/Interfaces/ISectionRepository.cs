using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishApi.Models;

namespace EnglishApi.Data.Interfaces
{
    public interface ISectionRepository:IBaseRepository<Section>
    {
        Task AddUserToSection(User user, Section section);
        Task<bool> IsHasAccess(User user, Section section);
        Task DeleteUserFromSection(User user, Section section);


        void AddSubsectionToSection(Subsection subsection, Section section);
        bool IsSubsectionInSection(Subsection subsection, Section section);
        Task DeleteSubsectionFromSection(Subsection subsection, Section section);//реализовать

        Task AddDictionaryToSection(Dictionary dictionary, Section section);
        Task DeleteDictionaryFromSection(Dictionary dictionary, Section section);//реализовать
        Task<bool> IsDictionaryInSection(Dictionary dictionary, Section section);




    }
}
