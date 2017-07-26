using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class Posts
    {
        public Posts()
        {
            Comments = new HashSet<Comments>();
            Locations = new HashSet<Locations>();
        }

        public int Postid { get; set; }
        public string Image { get; set; }
        public string Text { get; set; }
        public string Tags { get; set; }
        public DateTime? Date { get; set; }
        public int UserId { get; set; }

        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<Locations> Locations { get; set; }
        public virtual Users User { get; set; }
    }
}
