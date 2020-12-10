﻿using System.Threading.Tasks;

namespace English.Database.Data.Interfaces
{
    public interface IRepositoryManager
    {
        IDictionaryRepository Dictionary { get; }
        IWordRepository Word { get; }
        ISectionRepository Section { get; }
        ISubsectionRepository Subsection { get; }
        IUserRepository User { get; }
        IUserRoleRepository UserRole { get; }


        Task Save();
    }
}