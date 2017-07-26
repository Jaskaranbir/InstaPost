using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class Comments
    {
        public int Commentid { get; set; }
        public int Postid { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public int? Likeid { get; set; }
        public DateTime? Date { get; set; }

        public virtual Posts Post { get; set; }
        public virtual Users User { get; set; }
    }
}
