using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models {

    [BsonDiscriminator("tags")]
    public class Tags {

        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("tag")]
        public string Tag { get; set; }

        [BsonElement("t_postIds")]
        public int[] PostIds { get; set; }

        [BsonElement("t_count")]
        public int Count { get; set; }
    }

}
