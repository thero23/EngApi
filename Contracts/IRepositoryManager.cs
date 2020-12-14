using System.Threading.Tasks;

namespace Contracts
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
