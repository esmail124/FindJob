using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FindJob.Models
{
    public class RolePermissionViewModel
    {
        public string Title { get; set; }
        [Display(Name = "Selected Role")]
        public string SelectedRole { get; set; }
        public List<SelectListItem> Roles { get; set; }
    }
}
