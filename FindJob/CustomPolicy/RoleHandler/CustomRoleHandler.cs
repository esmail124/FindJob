using FindJob.Data.Entities;
using FindJob.Data.Repositories.PermissionRepository;
using FindJob.Data.Repositories.RoleRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using System.Security.Claims;

namespace FindJob.CustomPolicy.RoleHandler
{
    public class CustomRoleAuthorizationRequirement : IAuthorizationRequirement
    { }
    public class CustomRoleHandler : AuthorizationHandler<CustomRoleAuthorizationRequirement>
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IPermissionRepository permissionRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CustomRoleHandler(UserManager<AppUser> userManager, IPermissionRepository permissionRepository, IRoleRepository roleRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.permissionRepository = permissionRepository;
            this.roleRepository = roleRepository;
            this.httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRoleAuthorizationRequirement requirement)
        {
            //try
            //{
            //    //Log.Information($"start CustomRoleHandler -User: {context.User.GetUserName()}");

            //    RouteData route = httpContextAccessor.HttpContext?.GetRouteData();
            //    string path = (route?.Values.Count >= 3 ? $"{route.Values["area"]}/".ToLower() : "") + $"{route?.Values["controller"]}/{route?.Values["action"]}".ToLower();
            //    string[] routes = ["jobs/create", "Account/User/DocumentDefective", "Account/User/DocumentAdd", "Account/User/DocumentDelete"];

            //    if (routes.Any(r => r.ToLower().Equals(path)))
            //    {
            //        context.Succeed(requirement);
            //        return Task.CompletedTask;
            //    }

            //    var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
            //    string userId = userIdClaim.Value;
            //    AppUser user = userManager.FindByIdAsync(userId).Result;

            //    //if (user is { IsSystemAccount: true })
            //    //{
            //    //    context.Succeed(requirement);
            //    //    return Task.CompletedTask;
            //    //}


            //    //if (!context.User.IsActive() || string.IsNullOrEmpty(id))
            //    //{
            //    //    context.Fail();
            //    //    return Task.CompletedTask;
            //    //}

            //    if (string.IsNullOrEmpty(userId))
            //    {
            //        context.Fail();
            //        return Task.CompletedTask;
            //    }

            //    if (user is null)
            //    {
            //        context.Fail();
            //        return Task.CompletedTask;
            //    }

            //    AppRole role = roleRepository.GetByIdAsync(user.RoleId).Result;
            //    List<Permission> roots = (List<Permission>)permissionRepository.GetAllAsync().Result;

            //    if (!CheckAccess(roots))
            //    {
            //        context.Fail();
            //        return Task.CompletedTask;
            //    }

            //    context.Succeed(requirement);
            //    return Task.CompletedTask;

            //    bool CheckAccess(List<Permission> permissions)
            //    {
            //        foreach (Permission permission in permissions)
            //        {
            //            List<string> accessUrl = [permission.ClientAddress.ToLower()];
            //            if (permission.Urls != null)
            //            {
            //                accessUrl.AddRange(permission.Urls.Select(u => u.ToLower()));
            //            }

            //            if (role.PermissionIds.Any(r => r.Equals(permission.Id)) && accessUrl.Any(r => r.Equals(path)))
            //            {
            //                return true;
            //            }

            //            foreach (Data.Entities.Action permissionAction in permission.Actions)
            //            {
            //                accessUrl = [permissionAction.ClientAddress.ToLower()];
            //                accessUrl.AddRange(permissionAction.Urls.Select(u => u.ToLower()));

            //                if (role.PermissionIds.Any(r => r.Equals(permissionAction.Id)) && accessUrl.Any(r => r.Equals(path.ToLower())))
            //                {
            //                    return true;
            //                }
            //            }
            //        }

            //        return false;
            //    }
            //}
            //catch
            //{
            //    context.Fail();
            //    return Task.CompletedTask;
            //}

            try
            {
                //Console.WriteLine("In Custom Role Policy");
                //_logger.LogInformation($"start CustomRoleHandler -User: {context.User.Identity.GetName()}");

                RouteData route = httpContextAccessor.HttpContext?.GetRouteData();
                string path = (route?.Values.Count >= 3 ? $"{route.Values["area"]}/".ToLower() : "") + $"{route?.Values["controller"]}/{route?.Values["action"]}".ToLower();
                string[] routes = { "Account/SignUp", "Account/LogIn", "Account/LogOut","Home/Index" };

                if (routes.Any(r => r.ToLower().Equals(path)))
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }

                var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
                string userId = userIdClaim.Value;
                AppUser user = userManager.FindByIdAsync(userId).Result;

                //if (user is { IsSystemAccount: true })
                //{
                //    context.Succeed(requirement);
                //    return Task.CompletedTask;
                //}

                //if (!context.User.IsActive() || string.IsNullOrEmpty(id))
                //{
                //    context.Fail();
                //    return Task.CompletedTask;
                //}

                if (string.IsNullOrEmpty(userId))
                {
                    context.Fail();
                    return Task.CompletedTask;
                }

                if (user is null)
                {
                    context.Fail();
                    return Task.CompletedTask;
                }
                AppRole role = roleRepository.GetByIdAsync(user.RoleId).Result;
                List<Permission> roots = (List<Permission>)permissionRepository.GetAllAsync().Result;

                if (!CheckAccess(roots))
                {
                    context.Fail();
                    return Task.CompletedTask;
                }

                context.Succeed(requirement);
                return Task.CompletedTask;

                bool CheckAccess(List<Permission> permissions)
                {
                    foreach (Permission permission in permissions)
                    {
                        List<string> accessUrl = new List<string> { permission.ClientAddress.ToLower() };
                        if (permission.Urls != null)
                        {
                            accessUrl.AddRange(permission.Urls.Select(u => u.ToLower()));
                        }

                        if (role != null && role.Id.Equals(permission.Id) && accessUrl.Contains(path))
                        {
                            return true;
                        }

                        foreach (Data.Entities.Action permissionAction in permission.Actions)
                        {
                            accessUrl = new List<string> { permissionAction.ClientAddress.ToLower() };
                            accessUrl.AddRange(permissionAction.Urls.Select(u => u.ToLower()));

                            if (role.Id.Equals(permissionAction.Id) && accessUrl.Contains(path.ToLower()))
                            {
                                return true;
                            }
                        }
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error in CustomRoleHandler");
                context.Fail();
                return Task.CompletedTask;
            }
        }
    }
}
