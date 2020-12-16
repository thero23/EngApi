using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IDictionaryRepository
    {
        Task<IEnumerable<Dictionary>> GetAllDictionariesAsync(bool trackChanges);
        Task<IEnumerable<Dictionary>> GetDictionariesByConditionAsync(Expression<Func<Dictionary, bool>> expression, bool trackChanges);
        Task CreateDictionaryAsync(Dictionary dictionary);
        Task<IEnumerable<Dictionary>> GetByIdsAsync(IEnumerable<Guid> ids, bool
            trackChanges);
        void DeleteDictionary(Dictionary dictionary);
        void UpdateDictionary(Dictionary dictionary);
    }
}
