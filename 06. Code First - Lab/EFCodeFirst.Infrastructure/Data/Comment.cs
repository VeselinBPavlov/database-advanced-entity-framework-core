namespace EFCodeFirst.Infrastructure.Data
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Comments")]
    public class Comment
    {
        [Key]
        [Column("Id")]
        public int CommentId { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        [StringLength(250)]
        public string Content { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public User Author { get; set; }

        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; }
    }
}
