using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FindJob.Models
{
    public class UserRoleViewModel
    {
        [Required]
        public string Id { get; set; }
        [AllowNull]
        public string? Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public bool Selected { get; set; }
    }



}