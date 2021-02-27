using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using English.Services.Interfaces;
using Entities.Models;

namespace English.Services
{
    public class ExerciseAnswerService: IExerciseAnswerService
    {
        private readonly IRepositoryManager _repository;
        public ExerciseAnswerService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public IQueryable<Answer> FindAnswers(Expression<Func<User, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Exercise>> GetAllExercises(bool trackChanges)
        {
            return await _repository.Exercise.GetAllExercises(trackChanges);
        }

        public async Task<IEnumerable<Exercise>> GetExerciseByCondition(Expression<Func<Exercise, bool>> expression, bool trackChanges)
        {
            return await _repository.Exercise.GetExerciseByCondition(expression, trackChanges);
        }

        public async Task CreateExercise(Exercise exercise)
        {
            await _repository.Exercise.CreateExercise(exercise);
        }

        public void DeleteExercise(Exercise exercise)
        {
            _repository.Exercise.DeleteExercise(exercise);
        }

        public void UpdateExercise(Exercise exercise)
        {
            _repository.Exercise.UpdateExercise(exercise);
        }

        public async Task Save() => await _repository.Save();
    }
}
