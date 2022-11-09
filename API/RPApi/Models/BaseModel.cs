using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RPApi.Models
{
    public class BaseModel
    {
        [BsonIgnore]
        public int StatusCode { get; set; }

        [BsonIgnore]
        public string Message { get; set; }

    }
}
