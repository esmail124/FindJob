using FindJob.Data.Entities;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FindJob.Models
{
    public class RoleDTO
    {
        public string Name { get; set; }
        public ObjectId Id { get; set; }
        public AppRole Role { get; set; }
        public List<AppUser> Members { get; set; }
        public List<AppUser> NonMembers { get; set; }
    }

    public class RoleModification
    {
        [Required]
        public string RoleName { get; set; }

        public string RoleId { get; set; }

        public string[]? AddIds { get; set; }

        public string[]? DeleteIds { get; set; }
    }
}
