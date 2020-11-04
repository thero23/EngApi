﻿using System;
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
        private readonly EnglishContext _context; //сомнительно
        public SectionRepository(EnglishContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddUserToSection(User user, Section section)
        {
            var item = new SectionUser()
            {
                SectionId = section.Id,
                Section =  section,
                UserId = user.Id,
                User = user
            };

            await _context.SectionUsers.AddAsync(item);
            await _context.SaveChangesAsync();

        }

        public async Task AddSubsectionToSection(Subsection subsection, Section section)
        {
            
            subsection.SectionId = section.Id;
            await _context.SaveChangesAsync();

        }

        public bool IsSubsectionInSection(Subsection subsection, Section section)
        {
            return subsection.SectionId == section.Id;
        }

        public async Task DeleteUserFromSection(User user, Section section)
        {
            var item = _context.SectionUsers.SingleOrDefault(p => p.SectionId == section.Id && p.UserId == user.Id);
            _context.SectionUsers.Remove(item);
            await _context.SaveChangesAsync();

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

        public Task<bool> IsDictionaryInSection(Dictionary dictionary, Section section)
        {
            return _context.SectionDictionaries.AnyAsync(p=>p.DictionaryId==dictionary.Id && p.SectionId==section.Id);
        }
    }
}
