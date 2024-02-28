using AutoMapper;
using FindJob.Data.Entities;
using FindJob.Data.Repositories.PermissionRepository;
using FindJob.Data.Repository;
using FindJob.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace FindJob.Controllers
{
    //[Authorize(Policy = "CustomRolePolicy")]
    public class PermissionController : Controller
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;

        public PermissionController(IPermissionRepository permissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {

            List<Permission> permissions = _permissionRepository.GetAllAsync().Result.ToList();
            var permissionViewModel = _mapper.Map<List<PermissionViewModel>>(permissions);
            return View(permissionViewModel);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PermissionViewModel permissionViewModel)
        {
            if (ModelState.IsValid)
            {
                var permission = _mapper.Map<Permission>(permissionViewModel);
                await _permissionRepository.CreateAsync(permission);
                return RedirectToAction(nameof(Index));
            }

            return View(permissionViewModel);
        }
        public async Task<IActionResult> Update(ObjectId id)
        {
            var permission = await _permissionRepository.GetByIdAsync(id);
            var permissionViewModel = _mapper.Map<PermissionViewModel>(permission);
            if (permissionViewModel == null)
            {
                return NotFound();
            }

            return View(permissionViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Update(ObjectId id, PermissionViewModel permissionViewModel)
        {
            if (ModelState.IsValid)
            {
                var permission = _mapper.Map<Permission>(permissionViewModel);
                var result = _permissionRepository.UpdateAsync(id, permission);

            }

            return View(permissionViewModel);
        }

        public IActionResult Delete(ObjectId id, Permission permission)
        {
            permission = _permissionRepository.GetByIdAsync(id).Result;
            var permissionViewModel = _mapper.Map<PermissionViewModel>(permission);
            if (permissionViewModel == null)
            {
                return NotFound();
            }

            return View(permissionViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ObjectId id)
        {
            // Perform necessary validation and delete from the repository
            await _permissionRepository.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
