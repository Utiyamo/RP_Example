using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RPApi.Models
{
    public class Configurations : BaseModel
    {
        [BsonId]
        public ObjectId? Id { get; set; }

        [BsonElement("CodigoEmpresa")]
        public string CodEmpresa { get; set; }

        [BsonElement("Jwt")]
        public jwtModel jwtModel { get; set; }
    }

    public class jwtModel
    {
        [BsonElement("NameCorp")]
        public string NameCorporation { get; set; }

    }
}
