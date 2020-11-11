using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishApi.Data.Repositories;


namespace EnglishApi.Data.Interfaces
{
    public interface IRepositoryManager
    {
        IDictionaryRepository Dictionary { get; }
        IWordRepository Word { get; }
        IDictionaryWordRepository DictionaryWord { get; }
        ISectionRepository Section { get; }
        ISubsectionRepository Subsection { get; }
        IUserRepository User { get; }


        Task Save();
    }
}
