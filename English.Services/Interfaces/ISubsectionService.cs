﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace English.Services.Interfaces
{
    public interface ISubsectionService
    {
        IQueryable<Subsection> FindAllSubsections(bool trackChanges);
        IQueryable<Subsection> FindSubsectionByCondition(Expression<Func<Subsection, bool>> expression,
            bool trackChanges);
      
        Task CreateSubsection(Subsection entity);
        void UpdateSubsection(Subsection entity);
        void DeleteSubsection(Subsection entity);


        Task<IEnumerable<Exercise>> GetExercisesFromSubsection(Guid subsectionId);
        Task AddExerciseToSubsection(Guid exerciseId, Guid subsectionId);
        void RemoveExerciseFromSubsection(Guid exerciseId, Guid subsectionId);
        Task<bool> IsExerciseInSubsection(Guid exerciseId, Guid subsectionId);
        public Task<IEnumerable<Exercise>> GetExercisesNotInSubsection(Guid subsectionId);

        public Task Save();

    }
}
