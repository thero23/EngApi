﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Contracts
{

    public interface IBaseRepository<T>
    {

        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
            bool trackChanges);


        Task Create(T entity);
        void Update(T entity);
        void Delete(T entity);


    }



}
