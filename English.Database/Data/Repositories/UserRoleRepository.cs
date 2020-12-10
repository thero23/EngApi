using System;
using System.Collections.Generic;
using System.Text;
using English.Database.Data.Interfaces;
using English.Database.Models;

namespace English.Database.Data.Repositories
{
    class UserRoleRepository:BaseRepository<UserRole>,IUserRoleRepository
    {
        public UserRoleRepository(EnglishContext context) : base(context)
        {
        }
    }
}
