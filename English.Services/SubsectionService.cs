using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using English.Services.Interfaces;

namespace English.Services
{
    public class SubsectionService:ISubsectionService
    {
        private readonly IRepositoryManager _repository;

        public SubsectionService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public IQueryable<Subsection> FindAllSubsections(bool trackChanges)
        {
            return _repository.Subsection.FindAll(trackChanges);
        }

        public IQueryable<Subsection> FindSubsectionByCondition(Expression<Func<Subsection, bool>> expression, bool trackChanges)
        {
            return _repository.Subsection.FindByCondition(expression, trackChanges);
        }

      

        public async Task CreateSubsection(Subsection entity)
        {
            await _repository.Subsection.Create(entity);
        }

        public void UpdateSubsection(Subsection entity)
        {
            _repository.Subsection.Update(entity);
        }

        public void DeleteSubsection(Subsection entity)
        {
            _repository.Subsection.Delete(entity);
        }

        public async Task<IEnumerable<Exercise>> GetExercisesFromSubsection(Guid subsectionId)
        {
            return await _repository.Subsection.GetExercisesFromSubsection(subsectionId);
        }

        public async Task<IEnumerable<Exercise>> GetExercisesNotInSubsection(Guid subsectionId)
        {
            var exercises = (await _repository.Exercise.GetAllExercises(false)).Where(e=>!(_repository.Subsection.IsExerciseInSubsection(e.Id, subsectionId).Result));
            
            return exercises;
        }

        public async Task AddExerciseToSubsection(Guid exerciseId, Guid subsectionId)
        {
            await _repository.Subsection.AddExerciseToSubsection(exerciseId, subsectionId);
        }

        public void RemoveExerciseFromSubsection(Guid exerciseId, Guid subsectionId)
        {
            _repository.Subsection.RemoveExerciseFromSubsection(exerciseId, subsectionId);
        }

        public async Task<bool> IsExerciseInSubsection(Guid exerciseId, Guid subsectionId)
        {
            return await _repository.Subsection.IsExerciseInSubsection(exerciseId, subsectionId);
        }


        public async Task Save() => await _repository.Save();

    }
}
