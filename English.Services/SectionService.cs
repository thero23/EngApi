using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using English.Database.Data.Interfaces;
using English.Database.Models;
using English.Services.Interfaces;

namespace English.Services
{
    public class SectionService: ISectionService
    {
        private readonly IRepositoryManager _repository;

        public SectionService(IRepositoryManager repository)
        {
            _repository = repository;
        }
        public async Task<IQueryable<Section>> FindAllSections(bool trackChanges)
        {
            return await _repository.Section.FindAll(trackChanges);
        }

        public async Task<IQueryable<Section>> FindSectionByCondition(Expression<Func<Section, bool>> expression, bool trackChanges)
        {
            return await _repository.Section.FindByCondition(expression, trackChanges);
        }

        public async Task CreateSection(Section entity)
        {
           await _repository.Section.Create(entity);
        }

        public void UpdateSection(Section entity)
        {
            _repository.Section.Update(entity);
        }

        public void DeleteSection(Section entity)
        {
            _repository.Section.Delete(entity);
        }

        public async Task AddUserToSection(Guid userId, Guid sectionId)
        {
           await _repository.Section.AddUserToSection(userId, sectionId);
        }

        public async Task<bool> IsHasAccess(Guid userId, Guid sectionId)
        {
            return await _repository.Section.IsHasAccess(userId, sectionId);
        }

        public async Task DeleteUserFromSection(Guid userId, Guid sectionId)
        {
            await _repository.Section.DeleteUserFromSection(userId, sectionId);
        }

        public void AddSubsectionToSection(Guid subsectionId, Guid sectionId)
        {
            _repository.Section.AddSubsectionToSection(subsectionId, sectionId);
        }

        public bool IsSubsectionInSection(Guid subsectionId, Guid sectionId)
        {
            return _repository.Section.IsSubsectionInSection(subsectionId, sectionId);
        }

        public async Task DeleteSubsectionFromSection(Guid subsectionId, Guid sectionId)
        {
            await _repository.Section.DeleteSubsectionFromSection(subsectionId, sectionId);
        }

        public async Task AddDictionaryToSection(Guid dictionaryId, Guid sectionId)
        {
            await _repository.Section.AddDictionaryToSection(dictionaryId, sectionId);
        }

        public async Task DeleteDictionaryFromSection(Guid dictionaryId, Guid sectionId)
        {
            await _repository.Section.DeleteDictionaryFromSection(dictionaryId, sectionId);
        }

        public async Task<bool> IsDictionaryInSection(Guid dictionaryId, Guid sectionId)
        {
            
            return await _repository.Section.IsDictionaryInSection(dictionaryId, sectionId);
        }



        public async Task<bool> IsSectionExist(Guid sectionId)
        {
            var section = (await _repository.Section.FindByCondition(p => p.Id == sectionId, false)).FirstOrDefault();
            return section == null;
        }

        public async Task<bool> IsDictionaryExist(Guid dictionaryId)
        {
            var dictionary = (await _repository.Dictionary.FindByCondition(p => p.Id == dictionaryId, false)).FirstOrDefault();
            return dictionary == null;
        }

        public async Task<bool> IsUserExist(Guid userId)
        {
            var user = (await _repository.User.FindByCondition(p => p.Id == userId, false)).FirstOrDefault();
            return user == null;
        }

        public async Task<bool> IsSubsectionExist(Guid subsectionId)
        {
            var subsection = (await _repository.Subsection.FindByCondition(p => p.Id == subsectionId, false)).FirstOrDefault();
            return subsection == null;
        }





        public async Task Save() => await _repository.Save();

    }
}
