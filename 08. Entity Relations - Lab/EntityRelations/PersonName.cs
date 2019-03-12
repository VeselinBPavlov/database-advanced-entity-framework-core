namespace EntityRelations
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PersonName
    {
        [Column("FirstName")]
        [Required]
        public string FirstName { get; private set; }

        [Column("LastName")]
        [Required]
        public string LastName { get; private set; }

        public PersonName(string firstName, string lastName)
        {
            if (firstName.Length + lastName.Length > 100)
            {
                throw new ArgumentException("Name must be not longer than 100 symbols.");
            }

            this.FirstName = firstName;
            this.LastName = lastName;
        }

        [NotMapped]
        public string FullName => $"{this.FirstName} {this.LastName}";
    }
}