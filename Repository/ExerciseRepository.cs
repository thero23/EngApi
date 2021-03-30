using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Data;
using Entities.Migrations;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    class ExerciseRepository: BaseRepository<Exercise>, IExerciseRepository
    {
        private EnglishContext _context;

        public ExerciseRepository(EnglishContext context) : base(context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Exercise>> GetAllExercises(bool trackChanges)
        {
            return await FindAll(trackChanges).Include(p=>p.Answers).OrderBy(e=> e.Title).ToListAsync();
        }

        public async Task<IEnumerable<Exercise>> GetExerciseByCondition(Expression<Func<Exercise, bool>> expression, bool trackChanges)
        {
            return await FindByCondition(expression, trackChanges).Include(p => p.Answers).ToListAsync();
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
            var answers = _context.Exercises.Where(e => e.Id.Equals(exercise.Id)).Include(e=>e.Answers).Select(e=>e.Answers).FirstOrDefault();
            _context.Answers.RemoveRange(answers);
            Update(exercise);

        }
    }
}
