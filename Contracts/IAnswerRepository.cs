using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IAnswerRepository
    {
        Task<IEnumerable<Answer>> GetAnswersByExercise(Guid id, bool trackChanges);

    }

}
