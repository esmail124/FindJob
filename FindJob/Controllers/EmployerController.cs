using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FindJob.Controllers
{
   // [Authorize(Policy = "CustomRolePolicy")]
    public class EmployerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
