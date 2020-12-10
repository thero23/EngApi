using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using English.Database.Models;

namespace English.Services.Interfaces
{
    public interface ISectionService
    {
        IQueryable<Section> FindAllSections(bool trackChanges);
        IQueryable<Section> FindSectionByCondition(Expression<Func<Section, bool>> expression,
            bool trackChanges);
        Task CreateSection(Section entity);
        void UpdateSection(Section entity);
        void DeleteSection(Section entity);

        Task AddUserToSection(Guid userId, Guid sectionId);
        Task<bool> IsHasAccess(Guid userId, Guid sectionId);
        Task DeleteUserFromSection(Guid userId, Guid sectionId);


        void AddSubsectionToSection(Guid subsectionId, Guid sectionId);
        bool IsSubsectionInSection(Guid subsectionId, Guid sectionId);
        Task DeleteSubsectionFromSection(Guid subsectionId, Guid sectionId);//реализовать

        Task AddDictionaryToSection(Guid dictionaryId, Guid sectionId);
        Task DeleteDictionaryFromSection(Guid dictionaryId, Guid sectionId);//реализовать
        Task<bool> IsDictionaryInSection(Guid dictionaryId, Guid sectionId);


        bool IsSectionExist( Guid id);
        bool IsSubsectionExist(Guid id);
        bool IsUserExist(Guid id);
        bool IsDictionaryExist(Guid id);
       

        public Task Save();

    }
}
