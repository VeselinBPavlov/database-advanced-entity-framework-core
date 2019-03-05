using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCodeFirst.Infrastructure.Data
{
    [Table("Replies")]
    public class Reply
    {
        [Key]
        [Column("Id")]
        public int ReplyId { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public int CommentId { get; set; }

        [Required]
        [StringLength(250)]
        public string Content { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public User Author { get; set; }

        [ForeignKey(nameof(CommentId))]
        public Comment Comment { get; set; }
    }
}
