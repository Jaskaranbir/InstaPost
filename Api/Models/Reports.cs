using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models {

    [BsonDiscriminator("reports")]
    public class Reports {

        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("r_postId")]
        public int PostId { get; set; }

        [BsonElement("reports")]
        public Report[] ReportsArray { get; set; }

        [BsonElement("isResolved")]
        public bool IsResolved { get; set; }

        [BsonElement("r_count")]
        public int Count { get; set; }
    }

    public class Report {

        [BsonElement("r_userId")]
        public int UserId { get; set; }

        [BsonElement("r_comment")]
        public string Comment { get; set; }
    }

}
