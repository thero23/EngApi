using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishApi.Data.Interfaces;
using EnglishApi.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace EnglishApi.Data.Repositories
{
    public class SectionRepository:BaseRepository<Section>,ISectionRepository
    {
        
        public SectionRepository(EnglishContext context) : base(context)
        {
        }

        public async Task AddUserToSection(User user, Section section)
        {
            var item = new SectionUser()
            {
                SectionId = section.Id,
                UserId = user.Id
            };

            await _context.SectionUsers.AddAsync(item);
           

        }

        public void AddSubsectionToSection(Subsection subsection, Section section)
        {
            
            subsection.SectionId = section.Id;
           

        }

        public bool IsSubsectionInSection(Subsection subsection, Section section)
        {
            return subsection.SectionId.Equals(section.Id);
        }

        public Task DeleteSubsectionFromSection(Subsection subsection, Section section)
        {
            throw new NotImplementedException();

        }

        public async Task DeleteUserFromSection(User user, Section section)
        {
            var item = await _context.SectionUsers.SingleOrDefaultAsync(p => p.SectionId == section.Id && p.UserId == user.Id);
            _context.SectionUsers.Remove(item);
            

        }

       
        public async Task<bool> IsHasAccess(User user, Section section)
        {
            return await _context.SectionUsers.AnyAsync(p => p.SectionId == section.Id && p.UserId == user.Id);
        }

        public async Task AddDictionaryToSection(Dictionary dictionary, Section section)
        {
            var item = new SectionDictionary()
            {
                DictionaryId = dictionary.Id,
                SectionId = section.Id
            };

            await _context.SectionDictionaries.AddAsync(item);
        }

        public Task DeleteDictionaryFromSection(Dictionary dictionary, Section section)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsDictionaryInSection(Dictionary dictionary, Section section)
        {
            return _context.SectionDictionaries.AnyAsync(p=>p.DictionaryId==dictionary.Id && p.SectionId==section.Id);
        }
    }
}
