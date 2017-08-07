
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models {

    [BsonDiscriminator("followers")]
    public class Followers {

        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("follower_userId")]
        public int FollowerUserId { get; set; }

        [BsonElement("followed_userId")]
        public int FollowedUserId { get; set; }
    }

}
