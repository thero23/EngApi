using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using English.Database.Models;

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

        IQueryable<UserRole> FindAllUserRoles(bool trackChanges);
        IQueryable<UserRole> FindUserRoleByCondition(Expression<Func<UserRole, bool>> expression,
            bool trackChanges);
        Task CreateUserRole(UserRole entity);
        void UpdateUserRole(UserRole entity);
        void DeleteUserRole(UserRole entity);
        public Task Save();

    }
}
