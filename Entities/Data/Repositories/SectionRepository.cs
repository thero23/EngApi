using System;
using System.Linq;
using System.Threading.Tasks;
using Entities.Data.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities.Data.Repositories
{
    public class SectionRepository:BaseRepository<Section>,ISectionRepository
    {
        
        public SectionRepository(EnglishContext context) : base(context)
        {
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

        public void AddSubsectionToSection(Guid subsectionId, Guid sectionId)
        {
            var subsection = _context.Subsections.FirstOrDefault(p => p.Id == subsectionId);

            subsection.SectionId = sectionId;
           

        }

        public bool IsSubsectionInSection(Guid subsectionId, Guid sectionId)
        {
            var subsection = _context.Subsections.FirstOrDefault(p => p.Id == subsectionId);
            
            return subsection.SectionId.Equals(sectionId);
        }

        public Task DeleteSubsectionFromSection(Guid subsectionId, Guid sectionId)
        {
            throw new NotImplementedException();

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

        public Task DeleteDictionaryFromSection(Guid dictionaryId, Guid sectionId)
        {
            
            throw new NotImplementedException();
        }

        public Task<bool> IsDictionaryInSection(Guid dictionaryId, Guid sectionId)
        {
            return _context.SectionDictionaries.AnyAsync(p=>p.DictionaryId==dictionaryId && p.SectionId==sectionId);
        }
    }
}
