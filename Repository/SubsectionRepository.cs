using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Data;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class SubsectionRepository : BaseRepository<Subsection>, ISubsectionRepository
    {
        public SubsectionRepository(EnglishContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Exercise>> GetExercisesFromSubsection(Guid subsectionId)
        {
            var exerciseIds =
                (await _context.SubsectionExercises.Where(p => p.SubsectionId == subsectionId).ToListAsync())
                .Select(p => p.ExerciseId).ToList();


            return _context.Exercises.Where(p => exerciseIds.Contains(p.Id)).Include(e=>e.Answers);
        }

        public async Task AddExerciseToSubsection(Guid exerciseId, Guid subsectionId)
        {
            var item = new SubsectionExercise()
            {
                ExerciseId = exerciseId,
                SubsectionId = subsectionId
            };

            await _context.SubsectionExercises.AddAsync(item);
        }

        public void RemoveExerciseFromSubsection(Guid exerciseId, Guid subsectionId)
        {
            var item = _context.SubsectionExercises.FirstOrDefault(p =>
                p.SubsectionId.Equals(subsectionId) && p.ExerciseId.Equals(exerciseId));

           if(item != null) _context.SubsectionExercises.Remove(item);
        }

        public async Task<bool> IsExerciseInSubsection(Guid exerciseId, Guid subsectionId)
        {
            return await _context.SubsectionExercises.AnyAsync(p => p.SubsectionId.Equals(subsectionId) && p.ExerciseId.Equals(exerciseId));
        }

    }
    
}