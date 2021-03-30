using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ISubsectionRepository : IBaseRepository<Subsection>
    {
        Task <IEnumerable<Exercise>> GetExercisesFromSubsection(Guid subsectionId);
        Task AddExerciseToSubsection(Guid exerciseId, Guid subsectionId);
        void RemoveExerciseFromSubsection(Guid exerciseId, Guid subsectionId);
        Task<bool> IsExerciseInSubsection(Guid exerciseId, Guid subsectionId);
    }
}
