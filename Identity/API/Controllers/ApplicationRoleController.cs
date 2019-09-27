
using Identity.API.ViewModels;
using Identity.Application.Services;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Identity.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ApplicationRoleController : Controller
    {
        private readonly IRoleService _roleservice;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationRoleController(IRoleService  roleservice, UserManager<ApplicationUser> userManager )
        {
            _roleservice = roleservice;
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
            ApplicationRole applicationRole = await _roleservice.FindByNameAsync(model.ApplicationRoleName.ToUpper());
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
        [Route("AdduserRole")]
        public async Task<IActionResult> AdduserRole([FromBody]AddUserRoleRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ModelState);
            }
            IdentityResult roleResult;
            if (ModelState.IsValid)
            {
                ApplicationRole applicationRole = new ApplicationRole
                {
                    Name = model.RoleName,
                    CreatedDate = DateTime.UtcNow,
                    Description = model.Description,
                    IpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString()

                };
                roleResult = await _roleservice.CreateAsync(applicationRole);
                if (roleResult.Succeeded)
                {
                    return Ok("Role SuccessFully Added....");
                }

            }
            return Ok("UnExpected Error");

        }

        [HttpPost]
        [Route("UpdateUserRole")]
        public async Task<IActionResult> UpdateUserRole([FromBody]UpdateUserRoleRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ModelState);
            }
            bool isExist = !String.IsNullOrEmpty(model.Id);
            IdentityResult roleResult;


            if (ModelState.IsValid)
            {
                ApplicationRole applicationRole = await _roleservice.FindByIdAsync(model.Id);
                applicationRole.Name = model.RoleName;
                applicationRole.CreatedDate = DateTime.UtcNow;
                applicationRole.Description = model.Description;
                applicationRole.IpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                roleResult = await _roleservice.UpdateAsync(applicationRole);

                if (roleResult.Succeeded)
                {
                    if (isExist)
                    {
                        return Ok("Role SuccessFully Updated....");
                    }

                }
               
            }
            return Ok("UnExpected Error");
        }
               
               
        [HttpPost]
        [Route("DeleteUserRole")]
        public async Task<IActionResult> DeleteUserRole([FromBody]DeleteUserRoleRequest model)
        {

           if (!ModelState.IsValid)
            {
                return Ok(ModelState);
            }

            bool isExist = !String.IsNullOrEmpty(model.Id);
            IdentityResult roleResult;
            ApplicationRole applicationRole = await _roleservice.FindByIdAsync(model.Id);
            if (ModelState.IsValid)
            {
                if (isExist && _roleservice.FindByIdAsync(model.Id) != null)
                {
                    roleResult = _roleservice.DeleteAsync(applicationRole).Result;
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
            return Ok("UnExpected Eroor!!!!");

        }
    }
}
