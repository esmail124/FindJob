using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace FindJob.Data.Entities
{
    public abstract class BaseEntity : IEntity
    {
        [BsonElement("Name")]
        ////for convert json to bson in webapi =>[JsonPropertyName("Name")]
        public string EName { get; set; } = null!;
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get ; set ; }
    }

    public interface IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
    }
}
