using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.UIComponents
{
    public class UserMetaComp {
        [JsonProperty("followers")]
        public long Followers { get; set; }
        [JsonProperty("followings")]
        public long Followings { get; set; }
        [JsonProperty("postCount")]
        public long PostCount { get; set; }
    }
}
