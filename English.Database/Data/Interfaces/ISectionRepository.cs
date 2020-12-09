using System;
using System.Threading.Tasks;
using English.Database.Models;

namespace English.Database.Data.Interfaces
{
    public interface ISectionRepository:IBaseRepository<Section>
    {
        Task AddUserToSection(Guid userId, Guid sectionId);
        Task<bool> IsHasAccess(Guid userId, Guid sectionId);
        Task DeleteUserFromSection(Guid userId, Guid sectionId);


        void AddSubsectionToSection(Guid subsectionId, Guid sectionId);
        bool IsSubsectionInSection(Guid subsectionId, Guid sectionId);
        Task DeleteSubsectionFromSection(Guid subsectionId, Guid sectionId);//реализовать

        Task AddDictionaryToSection(Guid dictionaryId, Guid sectionId);
        Task DeleteDictionaryFromSection(Guid dictionaryId, Guid sectionId);//реализовать
        Task<bool> IsDictionaryInSection(Guid dictionaryId, Guid sectionId);




    }
}
