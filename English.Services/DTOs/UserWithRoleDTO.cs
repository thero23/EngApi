﻿using System;
using System.Collections.Generic;
using System.Text;

namespace English.Services.DTOs
{
    public class UserWithRoleDTO
    {
        public Guid Id { get; set; }

  
        public string Login { get; set; }

       
        public string Password { get; set; }


        public string Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string UserRoleName { get; set; }
    }
     
}
