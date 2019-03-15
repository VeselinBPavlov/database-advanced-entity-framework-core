using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFRelations.Infrastructure.Data.Model
{
    public class PersonName
    {
        public PersonName(string firstName, string lastName)
        {
            if (firstName.Length + lastName.Length > 99)
            {
                throw new ArgumentException("Name must be not longer than 100 symbols");
            }

            FirstName = firstName;
            LastName = lastName;
        }

        [Column("FirstName")]
        [Required]
        public string FirstName { get; private set; }

        [Column("LastName")]
        [Required]
        public string LastName { get; private set; }

        [NotMapped]
        public string FullName { get { return $"{FirstName} {LastName}"; } }
    }
}
