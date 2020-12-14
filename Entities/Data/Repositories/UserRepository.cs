using Entities.Data.Interfaces;
using Entities.Models;

namespace Entities.Data.Repositories
{
    public class UserRepository:BaseRepository<User>,IUserRepository
    {
        public UserRepository(EnglishContext context) : base(context)
        {
        }
    }
}
