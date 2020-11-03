using EnglishApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishApi.Data.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {

        Task Create(T entity);
        Task Delete(T entity);
        Task Update(Guid id, T entity);
        IEnumerable<T> GetAll();
        T GetById(Guid id);

    }
}
