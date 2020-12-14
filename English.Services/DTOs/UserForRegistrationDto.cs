using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace English.Services.DTOs
{
    public class UserForRegistrationDto
    {
        public string FirstName { get; set; }

        public string Lastname  { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        public string UserName  { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password  { get; set; }

        public string Email  { get; set; }

        public string PhoneNumber  { get; set; }

        public ICollection<string> Roles { get; set; }


    }
}
