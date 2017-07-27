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

        public int PostId { get; set; }
        public string PostImage { get; set; }
        public string PostText { get; set; }
        public string Tags { get; set; }
        public DateTime? PostDate { get; set; }
        public int UserId { get; set; }

        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<Locations> Locations { get; set; }
        public virtual Users User { get; set; }
    }
}
