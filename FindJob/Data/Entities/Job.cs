using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using System.ComponentModel.DataAnnotations;

namespace FindJob.Data.Entities
{
    [CollectionName("Jobs")]
    public class Job : BaseEntity
    {

        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string CompanyPhoneNumber { get; set; }

    }
}
