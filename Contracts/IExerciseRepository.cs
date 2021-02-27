using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IExerciseRepository
    {
        Task<IEnumerable<Exercise>> GetAllExercises(bool trackChanges);
        Task<IEnumerable<Exercise>> GetExerciseByCondition(Expression<Func<Exercise, bool>> expression, bool trackChanges);
        Task CreateExercise(Exercise exercise);
        void DeleteExercise(Exercise exercise);
        void UpdateExercise(Exercise exercise);
    }
}
