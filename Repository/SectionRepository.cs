using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts;
using Entities.Data;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class SectionRepository:BaseRepository<Section>,ISectionRepository
    {
        
        public SectionRepository(EnglishContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Section>> GetAllSectionsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(c => c.Order)
                .ToListAsync();



        public async Task<IEnumerable<Section>> GetSectionsByConditionAsync(Expression<Func<Section, bool>> expression, bool trackChanges)
        {
            return await FindByCondition(expression, trackChanges).ToListAsync();
        }


        public async Task CreateSectionAsync(Section section)
        {
            await Create(section);
        }

        public async Task<IEnumerable<Section>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            return await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();
        }

        public void DeleteSection(Section section)
        {
            Delete(section);
        }

        public void UpdateSection(Section section)
        {
            Update(section);
        }

        
        public async Task AddUserToSection(string userId, Guid sectionId)
        {
            var item = new SectionUser()
            {
                SectionId = sectionId,
                UserId = userId
            };
            await _context.SectionUsers.AddAsync(item);
        }

        public IQueryable<Dictionary> FindDictionariesInSection(Guid sectionId)
        {
            var dictionariesId =  _context.SectionDictionaries.Where(p => p.SectionId.Equals(sectionId)).Select(p => p.DictionaryId).ToList();

            return  _context.Dictionaries.Where(p => dictionariesId.Contains(p.Id));
           

        }

      

        public void AddSubsectionToSection(Guid subsectionId, Guid sectionId)
        {
            var subsection = _context.Subsections.FirstOrDefault(p => p.Id == subsectionId);

            subsection.SectionId = sectionId;
        }

        public async Task<bool> IsSubsectionInSection(Guid subsectionId, Guid sectionId)
        {
            var subsection = await _context.Subsections.FirstOrDefaultAsync(p => p.Id == subsectionId);
            
            return subsection.SectionId.Equals(sectionId);
        }

        public async Task DeleteSubsectionFromSection(Guid subsectionId)
        {
            var subsection =await _context.Subsections.FirstOrDefaultAsync(x => x.Id.Equals(subsectionId));
           
          
            subsection.SectionId = null;

        }


        public async Task DeleteUserFromSection(string userId, Guid sectionId)
        {
            var item = await _context.SectionUsers.SingleOrDefaultAsync(p => p.SectionId == sectionId && p.UserId == userId);
            _context.SectionUsers.Remove(item);
            

        }

       
        public async Task<bool> IsHasAccess(string userId, Guid sectionId)
        {
            return await _context.SectionUsers.AnyAsync(p => p.SectionId == sectionId && p.UserId == userId);
        }

        public async Task AddDictionaryToSection(Guid dictionaryId, Guid sectionId)
        {
            var item = new SectionDictionary()
            {
                DictionaryId = dictionaryId,
                SectionId = sectionId
            };

            await _context.SectionDictionaries.AddAsync(item);
        }

        public async Task DeleteDictionaryFromSection(Guid dictionaryId, Guid sectionId)
        {
            var item = await _context.SectionDictionaries.FirstOrDefaultAsync(x =>
                x.SectionId.Equals(sectionId) && x.DictionaryId.Equals(dictionaryId));
            _context.SectionDictionaries.Remove(item);
        }

        public Task<bool> IsDictionaryInSection(Guid dictionaryId, Guid sectionId)
        {
            return _context.SectionDictionaries.AnyAsync(p=>p.DictionaryId==dictionaryId && p.SectionId==sectionId);
        }
    }
}
