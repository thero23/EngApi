using System;
using System.Threading.Tasks;
using Entities.Models;

namespace Entities.Data.Interfaces
{
    public interface ISectionRepository:IBaseRepository<Section>
    {
        Task AddUserToSection(string userId, Guid sectionId);
        Task<bool> IsHasAccess(string userId, Guid sectionId);
        Task DeleteUserFromSection(string userId, Guid sectionId);


        void AddSubsectionToSection(Guid subsectionId, Guid sectionId);
        bool IsSubsectionInSection(Guid subsectionId, Guid sectionId);
        Task DeleteSubsectionFromSection(Guid subsectionId, Guid sectionId);//реализовать

        Task AddDictionaryToSection(Guid dictionaryId, Guid sectionId);
        Task DeleteDictionaryFromSection(Guid dictionaryId, Guid sectionId);//реализовать
        Task<bool> IsDictionaryInSection(Guid dictionaryId, Guid sectionId);




    }
}
