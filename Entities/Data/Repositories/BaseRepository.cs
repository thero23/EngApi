using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Entities.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Entities.Data.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected EnglishContext _context;

        public BaseRepository(EnglishContext context)
        {
            _context = context;
        }


        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges
                ? _context.Set<T>()
                    .AsNoTracking()
                : _context.Set<T>();


        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
            bool trackChanges) =>
            !trackChanges
                ? _context.Set<T>()
                    .Where(expression)
                    .AsNoTracking()
                : _context.Set<T>()
                    .Where(expression);

        public async Task Create(T entity) =>await _context.Set<T>().AddAsync(entity);
        public void Update(T entity) => _context.Set<T>().Update(entity);
        public void Delete(T entity) => _context.Set<T>().Remove(entity);

    }





}
