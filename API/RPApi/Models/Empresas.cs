using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RPApi.Models
{
    public class Empresas : BaseModel
    {
        [BsonId]
        public ObjectId? Id { get; set; }

        [BsonElement("CodigoEmpresa")]
        public string CodEmpresa { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
    }
}
