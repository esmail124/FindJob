using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace FindJob.Data.Entities
{
    public class Category : BaseEntity
    { 
    
        public string Details { get; set; }

    }
}
