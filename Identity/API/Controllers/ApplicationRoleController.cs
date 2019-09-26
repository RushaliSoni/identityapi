
using Identity.API.ViewModels;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Identity.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ApplicationRoleController : ControllerBase
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationRoleController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("SetOrRemoveUserRole")]
        public async Task<IActionResult> SetOrRemoveUserRole([FromBody]SetOrRemoveUserRoleRequest model)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);

            string roleName = string.Empty;
            if (user == null)
            {
                return Ok("User Not Found....");
            }

            string userName = user.UserName != null ? user.Email : user.UserName;

            // Find Role Using Role Name in AspNetRole Table
            ApplicationRole applicationRole = await _roleManager.FindByNameAsync(model.ApplicationRoleName.ToUpper());
            //ApplicationRole applicationRole = await _roleManager.FindByIdAsync(model.ApplicationRoleId);
            IdentityResult roleResult = null;

            if (applicationRole != null)
            {
                roleName = applicationRole.Name;

                if (model.SetOrRemove == EnSetRemoveRole.Set)
                {
                    // Add Role With Specific user
                    roleResult = await _userManager.AddToRoleAsync(user, applicationRole.Name);
                    return Ok($"SuccessFully Set Role {roleName} For User {userName}");
                }

                if (model.SetOrRemove == EnSetRemoveRole.Remove)
                {
                    // Remove Role With Specific user
                    roleResult = await _userManager.RemoveFromRoleAsync(user, applicationRole.Name);
                    return Ok($"SuccessFully Remove Role {roleName} For User {userName}");
                }
            }

            return Ok("UnExpected Errors !!!");
        }

        [HttpPost]
        [Route("AddOrUpdateOrDeleteUserRole")]
        public async Task<IActionResult> AddOrUpdateOrDeleteUserRole([FromBody]AddOrUpdateOrDeleteUserRoleRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ModelState);
            }

            bool isExist = !String.IsNullOrEmpty(model.Id);
            IdentityResult roleResult;
            ApplicationRole applicationRole = isExist ? await _roleManager.FindByIdAsync(model.Id) : new ApplicationRole { CreatedDate = DateTime.UtcNow };

            if (model.AddOrRemoveRole == EnAddOrRemoveRole.Delete)
            {
                if (isExist && _roleManager.FindByIdAsync(model.Id) != null)
                {
                    roleResult = _roleManager.DeleteAsync(applicationRole).Result;

                    if (roleResult.Succeeded)
                    {
                        return Ok("Role SuccessFully Deleted....");
                    }
                    else
                    {
                        return Ok("Failed To Delete Role....");
                    }
                }
            }
            else if (model.AddOrRemoveRole == EnAddOrRemoveRole.AddOrUpdate)
            {
                applicationRole.Name = model.RoleName.ToUpper();
                applicationRole.Description = model.Description;
                applicationRole.IpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                roleResult = isExist ? await _roleManager.UpdateAsync(applicationRole) : await _roleManager.CreateAsync(applicationRole);
                if (roleResult.Succeeded)
                {
                    if (isExist)
                    {
                        return Ok("Role SuccessFully Updated....");
                    }
                    else
                    {
                        return Ok("Role SuccessFully Added....");
                    }
                }
            }

            return Ok("UnExpected Error!!!");
        }

    }
}