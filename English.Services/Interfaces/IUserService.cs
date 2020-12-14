using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace English.Services.Interfaces
{
    public interface IUserService
    {

        IQueryable<User> FindAllUsers(bool trackChanges);
        IQueryable<User> FindUserByCondition(Expression<Func<User, bool>> expression,
            bool trackChanges);
        Task CreateUser(User entity);
        void UpdateUser(User entity);
        void DeleteUser(User entity);


        public Task Save();

    }
}
