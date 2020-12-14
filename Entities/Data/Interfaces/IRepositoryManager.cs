using System.Threading.Tasks;

namespace Entities.Data.Interfaces
{
    public interface IRepositoryManager
    {
        IDictionaryRepository Dictionary { get; }
        IWordRepository Word { get; }
        ISectionRepository Section { get; }
        ISubsectionRepository Subsection { get; }
        IUserRepository User { get; }



        Task Save();
    }
}
