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
    public class DictionaryRepository:BaseRepository<Dictionary>, IDictionaryRepository
    {
        public DictionaryRepository(EnglishContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Dictionary>> GetAllDictionariesAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToListAsync();



        public async Task<IEnumerable<Dictionary>> GetDictionariesByConditionAsync(Expression<Func<Dictionary, bool>> expression, bool trackChanges)
        {
            return await FindByCondition(expression, trackChanges).ToListAsync();

        }



        public async Task CreateDictionaryAsync(Dictionary dictionary)
        {
            await Create(dictionary);
        }

        public async Task<IEnumerable<Dictionary>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            return await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();
        }

        public void DeleteDictionary(Dictionary dictionary)
        {
            Delete(dictionary);
        }

        public void UpdateDictionary(Dictionary dictionary)
        {
            Update(dictionary);
        }

    }
}
