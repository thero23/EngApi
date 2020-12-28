﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.DTOs;

namespace Contracts
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();
    }
}
