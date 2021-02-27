using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Data;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    class ExerciseRepository: BaseRepository<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(EnglishContext context) : base(context)
        {
        }


        public async Task<IEnumerable<Exercise>> GetAllExercises(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(e=> e.Title).ToListAsync();
        }

        public async Task<IEnumerable<Exercise>> GetExerciseByCondition(Expression<Func<Exercise, bool>> expression, bool trackChanges)
        {
            return await FindByCondition(expression, trackChanges).ToListAsync();
        }

        public async Task CreateExercise(Exercise section)
        {
            await Create(section);
        }

        public void DeleteExercise(Exercise exercise)
        {
            Delete(exercise);
        }

        public void UpdateExercise(Exercise exercise)
        {
            Update(exercise);
        }
    }
}
