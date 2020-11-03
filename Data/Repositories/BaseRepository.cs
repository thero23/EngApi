using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishApi.Data.Interfaces;
using EnglishApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EnglishApi.Data.Repositories
{
    public class BaseRepository<T>:IBaseRepository<T> where T:BaseEntity
    {
        private readonly EnglishContext _context;

        public BaseRepository(EnglishContext context)
        {
            _context = context;
        }
        public async Task Create(T entity)
        {
            if (entity == null)
            {
                throw  new ArgumentNullException(nameof(entity));
            }
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            } 
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsEnumerable();
        }

        public T GetById(Guid id) 
        {
            
             return _context.Set<T>().FirstOrDefault(p => p.Id == id);
        }
    }
}
