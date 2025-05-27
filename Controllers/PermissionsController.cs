using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using System.Security.Claims;
using WebService.API.Authorization;
using WebService.API.Data.Entity;
using WebService.API.Helpers;
using WebService.API.Models.AuthModels;
using WebService.API.Models.UserModels;


namespace WebService.API.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin")]
    public class PermissionsController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public PermissionsController(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpGet("Permissions")]
        [Authorize(Permissions.Permissions_View)]
        public async Task<PermissionViewModel> GetRolesPermissions(string roleId)
        {
            var model = new PermissionViewModel();
            var allPermissions = new List<RoleClaimsViewModel>();
            allPermissions.GetPermissions(typeof(Permissions), roleId);
            //******
            var role = await _roleManager.FindByIdAsync(roleId);
            model.RoleId = roleId;
            var claims = await _roleManager.GetClaimsAsync(role);
            var allClaimValues = allPermissions.Select(a => a.Value).ToList();
            var roleClaimValues = claims.Select(a => a.Value).ToList();
            var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
            foreach (var permission in allPermissions)
            {
                if (authorizedClaims.Any(a => a == permission.Value))
                {
                    permission.Selected = true;
                }
            }
            model.RoleClaims = allPermissions;
            return model;
        }
        //[Authorize(Roles = "SuperAdmin")]
        [HttpPost("UpdatePermissions")]
        [Authorize(Permissions.Permissions_Update)]

        public async Task<string> UpdateRolesPermissions(PermissionViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);
            var claims = await _roleManager.GetClaimsAsync(role);
            foreach (var claim in claims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }
            var selectedClaims = model.RoleClaims.Where(a => a.Selected).ToList();
            foreach (var claim in selectedClaims)
            {
                await _roleManager.AddClaimAsync(role, new Claim(claim.Type, claim.Value));
            }
            return model.RoleId;
        }

        //[HttpPost("AddPermission/{Role}")]

        //public async Task<IActionResult> AddPermissions(string Role)
        //{

        //    if (Role != null)
        //    {
        //        switch (Role)
        //        {
        //            case "SuperAdmin":
        //                var superAdmin = await _roleManager.FindByNameAsync(Role);
        //                await _roleManager.AddClaimAsync(superAdmin, new Claim(CustomClaimTypes.Permission, Permissions.Users.View));
        //                await _roleManager.AddClaimAsync(superAdmin, new Claim(CustomClaimTypes.Permission, Permissions.Users.Edit));
        //                await _roleManager.AddClaimAsync(superAdmin, new Claim(CustomClaimTypes.Permission, Permissions.Users.Create));
        //                await _roleManager.AddClaimAsync(superAdmin, new Claim(CustomClaimTypes.Permission, Permissions.Users.Delete));
        //                await _roleManager.AddClaimAsync(superAdmin, new Claim(CustomClaimTypes.Permission, Permissions.Users.ViewById));
        //                break;
        //            case "Admin":
        //                var Admin = await _roleManager.FindByNameAsync(Role);
        //                await _roleManager.AddClaimAsync(Admin, new Claim(CustomClaimTypes.Permission, Permissions.Users.View));
        //                await _roleManager.AddClaimAsync(Admin, new Claim(CustomClaimTypes.Permission, Permissions.Users.Create));
        //                await _roleManager.AddClaimAsync(Admin, new Claim(CustomClaimTypes.Permission, Permissions.Users.Edit));
        //                await _roleManager.AddClaimAsync(Admin, new Claim(CustomClaimTypes.Permission, Permissions.Users.Delete));
        //                await _roleManager.AddClaimAsync(Admin, new Claim(CustomClaimTypes.Permission, Permissions.Users.ViewById));
        //                break;
        //        }

        //        var userRole = await _userManager.FindByNameAsync(Role);
        //        return Ok(userRole);
        //    }
        //    return BadRequest("Role not defined!");
        //}

    }
}
