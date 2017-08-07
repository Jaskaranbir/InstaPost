using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models {


    [BsonDiscriminator("bookmarks")]
    public class Bookmarks {

        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("b_userId")]
        public int UserId { get; set; }

        [BsonElement("b_postIds")]
        public int[] PostIds { get; set; }

        [BsonElement("b_count")]
        public int Count { get; set; }
    }

}
