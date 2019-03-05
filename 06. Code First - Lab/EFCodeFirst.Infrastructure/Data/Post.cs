namespace EFCodeFirst.Infrastructure.Data
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Posts")]
    public class Post
    {
        [Key]
        [Column("Id")]
        public int PostId { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 10)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public User Author { get; set; }
    }
}
