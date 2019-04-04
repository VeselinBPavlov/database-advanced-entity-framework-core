namespace VaporStore.DataProcessor.Import
{
    using System.ComponentModel.DataAnnotations;

    public class ImportUserDto
    {
        [Required]
        [RegularExpression(@"^[A-Z][a-z]+ [A-Z][a-z]+$")]
        public string FullName { get; set; }

        [Required]
        [MinLength(3), MaxLength(20)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Range(3, 103)]
        public int Age { get; set; }

        public ImportCardDto[] Cards { get; set; }
    }

    public class ImportCardDto
    {
        [Required]
        [RegularExpression(@"^[0-9]{4}\s+[0-9]{4}\s+[0-9]{4}\s+[0-9]{4}$")]
        public string Number { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{3}$")]
        public string CVC { get; set; }

        public string Type { get; set; }
    }
}
