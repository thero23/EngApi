using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ISectionRepository
    {
        Task<IEnumerable<Section>> GetAllSectionsAsync(bool trackChanges);
        Task<IEnumerable<Section>> GetSectionsByConditionAsync(Expression<Func<Section, bool>> expression, bool trackChanges);
        Task CreateSectionAsync(Section section);
        Task<IEnumerable<Section>> GetByIdsAsync(IEnumerable<Guid> ids, bool
            trackChanges);
        void DeleteSection(Section section);
        void UpdateSection(Section section);

        Task AddUserToSection(string userId, Guid sectionId);
        Task<bool> IsHasAccess(string userId, Guid sectionId);
        Task DeleteUserFromSection(string userId, Guid sectionId);


        public IQueryable<Dictionary> FindDictionariesInSection(Guid sectionId);
        void AddSubsectionToSection(Guid subsectionId, Guid sectionId);
        Task<bool> IsSubsectionInSection(Guid subsectionId, Guid sectionId);
        Task DeleteSubsectionFromSection(Guid subsectionId);



        Task AddDictionaryToSection(Guid dictionaryId, Guid sectionId);
        Task DeleteDictionaryFromSection(Guid dictionaryId, Guid sectionId);//реализовать
        Task<bool> IsDictionaryInSection(Guid dictionaryId, Guid sectionId);
        Task<IEnumerable<Section>> GetSectionsByUser(User user);




    }
}
