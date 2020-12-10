using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using English.Database.Data.Interfaces;
using English.Database.Models;
using English.Services.Interfaces;

namespace English.Services
{
    public class UserService:IUserService
    {

        private readonly IRepositoryManager _repository;
        public UserService(IRepositoryManager repository)
        {
            _repository = repository;
        }


        public IQueryable<User> FindAllUsers(bool trackChanges)
        {
            return  _repository.User.FindAll(trackChanges);
        }

        public IQueryable<User> FindUserByCondition(Expression<Func<User, bool>> expression, bool trackChanges)
        {
            return _repository.User.FindByCondition(expression, trackChanges);
        }

        public async Task CreateUser(User entity)
        {
            await _repository.User.Create(entity);
        }

        public void UpdateUser(User entity)
        {
            _repository.User.Update(entity);
        }

        public void DeleteUser(User entity)
        {
            _repository.User.Delete(entity);
        }

     
        public IQueryable<UserRole> FindAllUserRoles(bool trackChanges)
        {
            return _repository.UserRole.FindAll(trackChanges);
        }

        public IQueryable<UserRole> FindUserRoleByCondition(Expression<Func<UserRole, bool>> expression, bool trackChanges)
        {
            return _repository.UserRole.FindByCondition(expression, trackChanges);
        }

        public async Task CreateUserRole(UserRole entity)
        {
            await _repository.UserRole.Create(entity);
        }

        public void UpdateUserRole(UserRole entity)
        {
            _repository.UserRole.Update(entity);
        }

        public void DeleteUserRole(UserRole entity)
        {
            _repository.UserRole.Delete(entity);
        }

        public async Task Save() => await _repository.Save();
    }
}
