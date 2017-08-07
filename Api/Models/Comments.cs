using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("Comments")]
    public partial class Comments
    {
        [Key]
        public int CommentId { get; set; }
        [Column("PostId")]
        public int PostId { get; set; }
        [Column("UserId")]
        public int UserId { get; set; }
        [Column("CommentText")]
        public string CommentText { get; set; }
        [Column("CommentDate")]
        public DateTime CommentDate { get; set; }
        [Column("CommentTime")]
        public TimeSpan CommentTime { get; set; }
        [Column("HasChildComments")]
        public bool HasChildComments { get; set; }
        [Column("ParentCommentId")]
        public int? ParentCommentId { get; set; }

        [ForeignKey("ParentCommentId")]
        public virtual Comments ParentComment { get; set; }
        public virtual ICollection<Comments> InverseParentComment { get; set; }
        [ForeignKey("PostId")]
        public virtual Posts Post { get; set; }
        [ForeignKey("UserId")]
        public virtual Users User { get; set; }
    }
}
