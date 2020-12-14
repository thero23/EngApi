using Contracts;
using Entities.Data;
using Entities.Models;

namespace Repository
{
    public class UserRepository:BaseRepository<User>,IUserRepository
    {
        public UserRepository(EnglishContext context) : base(context)
        {
        }
    }
}
