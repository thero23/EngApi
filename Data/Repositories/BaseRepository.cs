using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EnglishApi.Data.Interfaces;
using EnglishApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EnglishApi.Data.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected EnglishContext _context;

        public BaseRepository(EnglishContext context)
        {
            _context = context;
        }


        public async Task<IQueryable<T>> FindAll(bool trackChanges) =>
            !trackChanges
                ? (await _context.Set<T>()
                    .AsNoTracking().ToListAsync()).AsQueryable()
                : (await _context.Set<T>().ToListAsync()).AsQueryable();


        public async Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression,
            bool trackChanges) =>
            !trackChanges
                ? ( await _context.Set<T>()
                    .Where(expression)
                    .AsNoTracking().ToListAsync()).AsQueryable()
                : (await _context.Set<T>()
                    .Where(expression).ToListAsync()).AsQueryable();

        public async Task Create(T entity) =>await _context.Set<T>().AddAsync(entity);
        public void Update(T entity) => _context.Set<T>().Update(entity);
        public void Delete(T entity) => _context.Set<T>().Remove(entity);

    }





}
