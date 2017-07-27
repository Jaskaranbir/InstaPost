using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class Comments
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string CommentText { get; set; }
        public DateTime? CommentDate { get; set; }

        public virtual Posts Post { get; set; }
        public virtual Users User { get; set; }
    }
}
