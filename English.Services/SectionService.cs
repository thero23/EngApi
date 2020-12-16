using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using English.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace English.Services
{
    public class SectionService: ISectionService
    {
        private readonly IRepositoryManager _repository;

        public SectionService(IRepositoryManager repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Section>> FindAllSections(bool trackChanges)
        {
            return  await _repository.Section.GetAllSectionsAsync(trackChanges);
        }

        public async Task<IEnumerable<Section>> FindSectionsByCondition(Expression<Func<Section, bool>> expression, bool trackChanges)
        {
            return await _repository.Section.GetSectionsByConditionAsync(expression, trackChanges);
        }

        public IEnumerable<Subsection> FindSubsectionsInSection(Guid sectionId, bool trackChanges)
        {
            return _repository.Subsection.FindByCondition(x => x.SectionId.Equals(sectionId),trackChanges);
        }
        public async Task<IEnumerable<Section>> FindSectionsByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            return await _repository.Section.GetByIdsAsync(ids, trackChanges);
        }

        public async Task CreateSection(Section entity)
        {
           await _repository.Section.CreateSectionAsync(entity);
        }

        public void UpdateSection(Section entity)
        {
            _repository.Section.UpdateSection(entity);
        }

        public void DeleteSection(Section entity)
        {
            _repository.Section.DeleteSection(entity);
        }

        public async Task AddUserToSection(string userId, Guid sectionId)
        {
           await _repository.Section.AddUserToSection(userId, sectionId);
        }

        public async Task<bool> IsHasAccess(string userId, Guid sectionId)
        {
            return await _repository.Section.IsHasAccess(userId, sectionId);
        }

        public async Task DeleteUserFromSection(string userId, Guid sectionId)
        {
            await _repository.Section.DeleteUserFromSection(userId, sectionId);
        }

    


        public void AddSubsectionToSection(Guid subsectionId, Guid sectionId)
        {
            _repository.Section.AddSubsectionToSection(subsectionId, sectionId);
        }

        public async Task<bool> IsSubsectionInSection(Guid subsectionId, Guid sectionId)
        {
            return await _repository.Section.IsSubsectionInSection(subsectionId, sectionId);
        }

        public async Task DeleteSubsectionFromSection(Guid subsectionId)
        {
            await _repository.Section.DeleteSubsectionFromSection(subsectionId);
        }


        public IEnumerable<Dictionary> FindDictionariesInSection(Guid sectionId)
        {
            return _repository.Section.FindDictionariesInSection(sectionId);
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
            var section = (await _repository.Section.GetSectionsByConditionAsync(p => p.Id == sectionId, false)).FirstOrDefault();
            return section != null;
        }

        public  async Task<bool> IsDictionaryExist(Guid dictionaryId)
        {
            var dictionary = (await _repository.Dictionary.GetDictionariesByConditionAsync(p => p.Id == dictionaryId, false)).FirstOrDefault();
            return dictionary != null;
        }

        public bool IsUserExist(string userId)
        {
            var user = _repository.User.FindByCondition(p => Equals(p.Id, userId), false).FirstOrDefault();
            return user != null;
        }

        public bool IsSubsectionExist(Guid subsectionId)
        {
            var subsection = _repository.Subsection.FindByCondition(p => p.Id == subsectionId, false).FirstOrDefault();
            return subsection != null;
        }





        public async Task Save() => await _repository.Save();

    }
}
