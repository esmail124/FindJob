using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Action = FindJob.Data.Entities.Action;

namespace FindJob.Models
{
    public class PermissionViewModel
    {
        [AllowNull]
        public ObjectId? Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ClientAddress { get; set; }
        [AllowNull]
        public List<Action>? Actions { get; set; }
        [MaxLength(30)]
        public List<string> Urls { get; set; }
    }
}
