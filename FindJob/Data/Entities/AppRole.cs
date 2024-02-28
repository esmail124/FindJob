using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace FindJob.Data.Entities
{
    [CollectionName("Roles")]
    public class AppRole : MongoIdentityRole<ObjectId>,IEntity
    {
        public List<ObjectId> PermissionIds { get; set; } = new List<ObjectId>();
    }
}
