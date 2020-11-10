using EnglishApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EnglishApi.Data.Interfaces
{
  
    public interface IBaseRepository<T>
    {

        Task<IQueryable<T>> FindAll(bool trackChanges);
        Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression,
            bool trackChanges);
        Task Create(T entity);
        void Update(T entity);
        void Delete(T entity);


    }

    

}
