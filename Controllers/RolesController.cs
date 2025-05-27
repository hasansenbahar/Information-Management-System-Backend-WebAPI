using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.API.Authorization;
using WebService.API.Data.Entity;
using WebService.API.Models.UserModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebService.API.Controllers
{
    [Route("api")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin, Admin")]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public RolesController(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // /api/Roles

        [HttpGet("Roles")]
        //[Authorize(Permissions.Roles.View)]
        //[Authorize(AuthenticationSchemes = "Bearer",Roles = "SuperAdmin,Admin,")]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return Ok(new UserResponseManager
            {
                IsSuccess = true,
                Message = roles
            });
        }



        // /api/Roles/{RoleName}

        [HttpPost("AddRole/role")]
        [Authorize(Permissions.Roles_Create)]
        //[Authorize(Permissions.Users.SuperAdminCreate)]
        //[Authorize(AuthenticationSchemes = "Bearer", /*Policy = "SuperAdmin",*/ Roles = "SuperAdmin")]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (roleName != null)
            {
                var newRole = await _roleManager.CreateAsync(new Role(roleName.Trim()));
            }
            return Ok(new UserResponseManager
            {
                IsSuccess = true,
                Message = "Role '" + roleName + "' has been added to Role Manager!"
            });
        }


        // /api/Roles/{RoleName}

        [HttpDelete("RemoveRole/role")]
        //[Authorize(Permissions.Users.Delete)]
        [Authorize(Permissions.Roles_Delete)]
        public async Task<IActionResult> RemoveRole(string roleName)
        {
            if (roleName != null)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                var removeRole = await _roleManager.DeleteAsync(role);
            }
            return Ok(new UserResponseManager
            {
                IsSuccess = true,
                Message = "Role '" + roleName + "' has been Removed from Role Manager!"
            });
        }

        // /api/userRoles/{id}

        [HttpGet("UserRoles/userId")]
        //[Authorize(Permissions.Users.viewById)]
        //[Authorize(AuthenticationSchemes = "Bearer", /*Policy = "SuperAdmin",*/ Roles = "SuperAdmin")]
        [Authorize(Permissions.Roles_ViewById)]
        public async Task<IActionResult> GetUserRolebyId(string userId)
        {
            var existingUser = await _userManager.FindByIdAsync(userId);
            if (existingUser != null)
            {
                var roles = await _userManager.GetRolesAsync(existingUser);
                return Ok(roles);
            };
            return BadRequest("User not found!");

        }

        // /api/AddUserRole/{RoleName}

        [HttpPost("AddUserRole/userRole")]
        //[Authorize(Permissions.Users.Create)]
        //[Authorize(AuthenticationSchemes = "Bearer", /*Policy = "SuperAdmin",*/ Roles = "SuperAdmin")]
        [Authorize(Permissions.Users_Role_Create)]
        public async Task<IActionResult> AddUserRole(string userId, string userRole)
        {
            var existingUser = await _userManager.FindByIdAsync(userId);
            if (userRole != null)
            {
                if (await _roleManager.RoleExistsAsync(userRole))
                {
                    await _userManager.AddToRoleAsync(existingUser, userRole);
                    var addedRoles = await _userManager.GetRolesAsync(existingUser);
                    return Ok(addedRoles);
                };
                return BadRequest("Role does not exits!");
            }
            return NotFound("Please fill the required fields ! ");
        }

        // /api/RemoveUserRole/{RoleName}

        [HttpDelete("RemoveUserRole/userRole")]
        [Authorize(Permissions.Users_Role_Delete)]
        public async Task<IActionResult> RemoveUserRole(string userId, string userRole)
        {
            var existingUser = await _userManager.FindByIdAsync(userId);
            if (userRole != null)
            {
                if (await _roleManager.RoleExistsAsync(userRole))
                {
                    await _userManager.RemoveFromRoleAsync(existingUser, userRole);
                    var addedRoles = await _userManager.GetRolesAsync(existingUser);
                    return Ok(addedRoles);
                };
                return BadRequest("Role does not exits!");
            }
            return NotFound("Please fill the required fields ! ");
        }


    }
}
