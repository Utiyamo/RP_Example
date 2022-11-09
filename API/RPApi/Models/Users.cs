using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RPApi.Models
{
    public class Users : BaseModel
    {

        [BsonId]
        public ObjectId? Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Username")]
        public string Username { get; set; }

        [BsonElement("Email")]
        public string? Email { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }
    }
}
