using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebService.API.Constants;
using WebService.API.Data.Entity;
using WebService.API.Models;

namespace WebService.API.Authorization
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        UserManager<User> _userManager;
        RoleManager<Role> _roleManager;
        private readonly IConfiguration _config;

        public PermissionAuthorizationHandler(UserManager<User> userManager, RoleManager<Role> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = configuration;
        }

        protected override async Task<ApiResponse> HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User != null)
            {
                // Get all the roles the user belongs to and check if any of the roles has the permission required
                // for the authorization to succeed.

                var user = await _userManager.GetUserAsync(context.User);
                if (user != null)
                {
                    var userRoleNames = await _userManager.GetRolesAsync(user);
                    var userRoles = _roleManager.Roles.Where(x => userRoleNames.Contains(x.Name));

                    foreach (var role in userRoles)
                    {
                        if (role.Name == Roles.SuperAdmin.ToString())
                        {
                            context.Succeed(requirement);
                            return new ApiResponse
                            {
                                IsSuccess = true,
                                Result = TextResources.Authorized
                            };
                        }
                        var roleClaims = await _roleManager.GetClaimsAsync(role);
                        var permissions = roleClaims.Where(x => x.Type == CustomClaimTypes.Permission &&
                                                                x.Value == requirement.Permission &&
                                                                x.Issuer == "LOCAL AUTHORITY")
                                                    .Select(x => x.Value);

                        if (permissions.Any())
                        {
                            context.Succeed(requirement);
                            return new ApiResponse
                            {
                                IsSuccess = true,
                                Result = TextResources.Authorized
                            };
                        }

                    }
                }

            };

            return new ApiResponse
            {
                IsSuccess = false,
                Result = TextResources.NotAuthenticated
            };
        }
    }
}

