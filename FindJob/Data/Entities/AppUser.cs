using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FindJob.Data.Entities
{
    [CollectionName("Users")]
    public class AppUser : MongoIdentityUser<ObjectId>, IEntity
    {
        public AppUser()
        {
            IsActive = true;
        }

        public string Address { get; set; }
        public bool IsActive { get; set; }
        public ObjectId RoleId { get; set; }
        public string FullName { get; set; }
        public GenderType Gender { get; set; }
        public int Age { get; set; }
        public List<AppRole> UserRoleList { get; set; }


    }

    public enum GenderType
    {
        [Display(Name = "مرد")]
        Male = 1,

        [Display(Name = "زن")]
        Female = 2
    }


}
