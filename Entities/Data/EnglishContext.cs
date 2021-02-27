using System;
using System.Linq;
using Entities.Data.Configuration;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities.Data
{
    public class EnglishContext:IdentityDbContext<User>
    {
        public EnglishContext(DbContextOptions options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
                //builder.ApplyConfiguration(new RoleConfiguration());
        }

        public DbSet<Word> Words{ get; set; }
        public DbSet<Dictionary> Dictionaries{ get; set; }
        public DbSet<DictionaryWord> DictionaryWords{ get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Subsection> Subsections { get; set; }
        public DbSet<SectionDictionary> SectionDictionaries{ get; set; }
        public DbSet<SectionUser> SectionUsers { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Exercise> Exercises { get; set; }

    }
}
