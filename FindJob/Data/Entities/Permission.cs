using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FindJob.Data.Entities
{
    public class Permission:BaseEntity
    {
        public string ClientAddress { get; set; }
        public List<string> Urls { get; set; }
        [Required]
        public string Title { get; set; }

        [AllowNull]
        public List<Action>? Actions { get; set; }

    }


    public class Action
    {
        public ObjectId Id { get; set; }

        public string ClientAddress { get; set; }
        public List<string> Urls { get; set; }
    }
}
