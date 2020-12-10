using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace English.Database.Data
{
    public class PasswordHashSettings : IValidatableObject
    {
        public string HashKey { get; set; }

        public PasswordHashSettings() { }
        public PasswordHashSettings(PasswordHashSettings settings)
        {
            HashKey = settings.HashKey;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (string.IsNullOrWhiteSpace(HashKey))
            {
                HashKey = "English";
                errors.Add(new ValidationResult(
                    $"Для параметра {GetType().Name} -> HashKey задано значение по умолчанию '{HashKey}'"));
            }

            return errors;
        }
    }
}
