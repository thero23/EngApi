using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using English.Database.Models;

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
        public Task Save();

    }
}
