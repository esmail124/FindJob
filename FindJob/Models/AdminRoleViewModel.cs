using System.ComponentModel.DataAnnotations;

namespace FindJob.Models
{
    public class AdminRoleViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<PermissionCheckboxViewModel> Permissions { get; set; } = new List<PermissionCheckboxViewModel>();
    }
    public class PermissionCheckboxViewModel
    {
        public string PermissionId { get; set; }
        public string Title { get; set; }
        public bool IsSelected { get; set; }
    }
}
