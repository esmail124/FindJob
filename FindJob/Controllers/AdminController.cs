using AutoMapper;
using FindJob.Data.Entities;
using FindJob.Data.Repositories.PermissionRepository;
using FindJob.Data.Repositories.RoleRepository;
using FindJob.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace FindJob.Controllers
{
    //[Authorize(Policy = "CustomRolePolicy")]
    public class AdminController : Controller
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;

        public AdminController(IRoleRepository roleRepository, IPermissionRepository permissionRepository, RoleManager<AppRole> roleManager, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _roleManager = roleManager;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (_roleManager == null || _permissionRepository == null)
            {
                // Handle the case where dependencies are not initialized
                // You might want to log an error or return an appropriate response
                return View(new List<AdminRoleViewModel>());
            }

            var roles = _roleManager.Roles.ToList();
            var permissions = await _permissionRepository.GetAllAsync();

            var viewModelList = new List<AdminRoleViewModel>();

            foreach (var role in roles)
            {
                var permissionViewModels = permissions.Select(permission => new PermissionCheckboxViewModel
                {
                    PermissionId = permission.Id.ToString(),
                    Title = permission.Title,
                    IsSelected = role.PermissionIds.Contains(permission.Id)
                }).ToList();

                var viewModel = new AdminRoleViewModel
                {
                    RoleId = role.Id.ToString(),
                    RoleName = role.Name,
                    Permissions = permissionViewModels
                };

                viewModelList.Add(viewModel);
            }

            return View(viewModelList);
        }

        [HttpPost]
        public async Task<IActionResult> Index(List<AdminRoleViewModel> viewModelList)
        {
            try
            {
                if (viewModelList != null && viewModelList.Any() && _roleManager != null)
                {
                    foreach (var viewModel in viewModelList)
                    {
                        var role = await _roleManager.FindByIdAsync(viewModel.RoleId);

                        if (role != null)
                        {
                            // Update the role's permissions based on the selected checkboxes
                            role.PermissionIds = viewModel.Permissions
                                .Where(p => p.IsSelected)
                                .Select(p => ObjectId.Parse(p.PermissionId))
                                .ToList();

                            // Save the changes to the role
                            await _roleManager.UpdateAsync(role);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately (e.g., display an error message)
            }

            // Redirect back to the Index action to refresh the view
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(AdminRoleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                var role = new AppRole
                {
                    Name = viewModel.RoleName
                };
                await _roleRepository.CreateAsync(role);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> EditRole(ObjectId id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role != null)
            {
                return View(role);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(ObjectId id, AppRole role)
        {
            if (id != role.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _roleRepository.UpdateAsync(role.Id,role);
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        public async Task<IActionResult> DetailsRole(ObjectId id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        public async Task<IActionResult> DeleteRole(ObjectId id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        [HttpPost, ActionName("DeleteRole")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRoleConfirmed(ObjectId id)
        {
            await _roleRepository.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        //public IActionResult CreatePermission()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreatePermission(Permission permission)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _permissionRepository.CreateAsync(permission);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(permission);
        //}

        //public async Task<IActionResult> EditPermission(string id)
        //{
        //    var permission = await _permissionRepository.GetAsync(id);
        //    if (permission == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(permission);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditPermission(string id, Permission permission)
        //{
        //    if (id != permission.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        await _permissionRepository.UpdateAsync(permission);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(permission);
        //}

        //public async Task<IActionResult> DetailsPermission(string id)
        //{
        //    var permission = await _permissionRepository.GetAsync(id);
        //    if (permission == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(permission);
        //}

        //public async Task<IActionResult> DeletePermission(string id)
        //{
        //    var permission = await _permissionRepository.GetAsync(id);
        //    if (permission == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(permission);
        //}

        //[HttpPost, ActionName("DeletePermission")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeletePermissionConfirmed(string id)
        //{
        //    await _permissionRepository.DeleteAsync(id);
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
