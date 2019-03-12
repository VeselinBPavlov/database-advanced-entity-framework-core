namespace EntityRelations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System;

    public class Student
    {
        public int StudentId { get; set; }

        [NotMapped]
        public PersonName Name { get; set; }

        public string Phone { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime? Birthday { get; set; }
    }
}
