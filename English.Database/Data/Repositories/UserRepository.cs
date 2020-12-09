using English.Database.Data.Interfaces;
using English.Database.Models;

namespace English.Database.Data.Repositories
{
    public class UserRepository:BaseRepository<User>,IUserRepository
    {
        public UserRepository(EnglishContext context) : base(context)
        {
        }
    }
}
