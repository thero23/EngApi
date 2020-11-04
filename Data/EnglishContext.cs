using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishApi.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace EnglishApi.Data
{
    public class EnglishContext:DbContext
    {
        public EnglishContext(DbContextOptions<EnglishContext> options):base(options)
        {
            
        }

        public DbSet<Word> Words{ get; set; }
        public DbSet<Dictionary> Dictionaries{ get; set; }
        public DbSet<DictionaryWord> DictionaryWords{ get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Subsection> Subsections { get; set; }
        public DbSet<SectionDictionary> SectionDictionaries{ get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<SectionUser> SectionUsers { get; set; }


    }
}
