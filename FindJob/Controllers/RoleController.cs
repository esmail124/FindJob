using AutoMapper;
using FindJob.Data.Entities;
using FindJob.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace TripPJ.UI.Controllers
{
    //[Authorize(Policy = "CustomRolePolicy")]
    public class RoleController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<RoleController> _logger;
        private readonly RoleManager<AppRole> _roleManager;
        public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, IMapper mapper, ILogger<RoleController> logger)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _roleManager = roleManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var roles = _roleManager.Roles.ToList();
            var rolesView = _mapper.Map<List<RoleDTO>>(roles);
            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> AddUser(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                ViewData["RoleId"] = id;
                var users = _userManager.Users.ToList();
                var usersDto = _mapper.Map<List<UserRoleViewModel>>(users);
                return View(usersDto);
            }
            return Forbid();
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(string id, IEnumerable<UserRoleViewModel> userRoleView)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid role id");
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound("Role not found");
            }

            if (userRoleView == null || !userRoleView.Any(c => c.Selected))
            {
                ModelState.AddModelError("", "Choose at least one user to add to the role.");
                return View();
            }

            if (ModelState.IsValid)
            {
                var usersToAdd = userRoleView.Where(c => c.Selected).ToList();
                var users = _mapper.Map<List<AppUser>>(usersToAdd);

                foreach (var user in users)
                {
                    user.SecurityStamp = null;
                    var result = await _userManager.AddToRoleAsync(user, role.Name);

                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", $"Error adding user {user.UserName} to the role: {result.Errors.FirstOrDefault()?.Description}");
                        // Handle the error as needed
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "Model is not valid");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleDTO roleView)
        {
            if (ModelState.IsValid)
            {
                var role = _mapper.Map<AppRole>(roleView);
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            ModelState.AddModelError("", "It is not valid");
            return View(roleView);
        }
        [HttpGet]
        public async Task<IActionResult> Update(ObjectId id)
        {
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                var role = await _roleManager.FindByIdAsync(id.ToString());
                var roleDto = _mapper.Map<RoleDTO>(role);
                return View(roleDto);
            }
            return Forbid();
        }
        [HttpPost]
        public async Task<IActionResult> Update(ObjectId id, RoleDTO roleView)
        {
            if (ModelState.IsValid)
            {
                var role = _mapper.Map<AppRole>(roleView);
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            ModelState.AddModelError("", "It is not valid");
            return View(roleView);
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var role = await _roleManager.FindByIdAsync(id);

                if (role != null)
                {
                    var result = await _roleManager.DeleteAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    // Handle the case where the role deletion fails
                    ModelState.AddModelError("", "Role deletion failed.");
                }
            }
            return NotFound();
        }
    }
}
