using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Api.Models {
    public partial class Posts {
        public Posts() {
            Comments = new HashSet<Comments>();
        }

        [JsonProperty("postId")]
        public int PostId { get; set; }
        [JsonProperty("postImage")]
        public string PostImage { get; set; }
        [JsonProperty("postText")]
        public string PostText { get; set; }
        [JsonProperty("postDate")]
        public DateTime PostDate { get; set; }
        [JsonProperty("postTime")]
        public TimeSpan PostTime { get; set; }
        [JsonProperty("commentsCount")]
        public int CommentsCount { get; set; }
        [JsonProperty("likesCount")]
        public int LikesCount { get; set; }
        [JsonProperty("userId")]
        public int UserId { get; set; }

        public virtual ICollection<Comments> Comments { get; set; }
        public virtual Locations Locations { get; set; }
        public virtual Users User { get; set; }
    }
}
