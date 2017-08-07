using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models {

    [BsonDiscriminator("likes")]
    public class Likes {

        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("l_postId")]
        public int PostId { get; set; }

        [BsonElement("l_userIds")]
        public int[] UserIds { get; set; }
    }

}
