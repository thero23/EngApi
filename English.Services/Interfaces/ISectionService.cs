using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace English.Services.Interfaces
{
    public interface ISectionService
    {
        Task<IEnumerable<Section>> FindAllSections(bool trackChanges);
        Task<IEnumerable<Section>> FindSectionsByCondition(Expression<Func<Section, bool>> expression,
            bool trackChanges);

        Task<IEnumerable<Section>> FindSectionsByUser(User user);
        Task<IEnumerable<Section>> FindSectionsByIds(IEnumerable<Guid> ids, bool trackChanges);
        IEnumerable<Subsection> FindSubsectionsInSection(Guid sectionId, bool trackChanges);

        Task CreateSection(Section entity);
        void UpdateSection(Section entity);
        void DeleteSection(Section entity);

        Task AddUserToSection(string userId, Guid sectionId);
        Task<bool> IsHasAccess(string userId, Guid sectionId);
        Task DeleteUserFromSection(string userId, Guid sectionId);


        IEnumerable<Dictionary> FindDictionariesInSection(Guid sectionId);

        void AddSubsectionToSection(Guid subsectionId, Guid sectionId);
        Task<bool> IsSubsectionInSection(Guid subsectionId, Guid sectionId);
        Task DeleteSubsectionFromSection(Guid subsectionId);


        Task AddDictionaryToSection(Guid dictionaryId, Guid sectionId);
        Task DeleteDictionaryFromSection(Guid dictionaryId, Guid sectionId);
        Task<bool> IsDictionaryInSection(Guid dictionaryId, Guid sectionId);


        Task<bool> IsSectionExist( Guid id);
        bool IsSubsectionExist(Guid id);
        bool IsUserExist(string id);
        Task<bool> IsDictionaryExist(Guid id);
       

        public Task Save();

    }
}
