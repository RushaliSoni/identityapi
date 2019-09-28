
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

        public ApplicationRoleController(IRoleService  roleservice )
        {
            _roleservice = roleservice;
        }

        [HttpPost]
        [Route("SetUserRole")]
        public async Task<IActionResult> SetUserRole([FromBody]SetUserRoleRequest model)
        {
            ApplicationUser user = await _roleservice.FindByEmailAsync(model.Email);

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

                if (ModelState.IsValid)
                {
                    // Add Role With Specific user
                    roleResult = await _roleservice.AddToRoleAsync(user, applicationRole.Name);
                    return Ok($"SuccessFully Set Role {roleName} For User {userName}");
                }

                
            }

            return Ok("UnExpected Errors !!!");
        }
        [HttpPost]
        [Route("RemoveUserRole")]
        public async Task<IActionResult> RemoveUserRole([FromBody]RemoveUserRoleRequest model)
        {
            ApplicationUser user = await _roleservice.FindByEmailAsync(model.Email);

            string roleName = string.Empty;
            if (user == null)
            {
                return Ok("User Not Found....");
            }

            string userName = user.UserName != null ? user.Email : user.UserName;

            // Find Role Using Role Name in AspNetRole Table
            ApplicationRole applicationRole = await _roleservice.FindByNameAsync(model.ApplicationRoleName.ToUpper());
            IdentityResult roleResult = null;

            if (ModelState.IsValid)
            {
                roleName = applicationRole.Name;

               
                    // Remove Role With Specific user
                    roleResult = await _roleservice.RemoveFromRoleAsync(user, applicationRole.Name);
                    return Ok($"SuccessFully Remove Role {roleName} For User {userName}");
                
            }

            return Ok("UnExpected Errors !!!");
        }
        [HttpPost]
        [Route("AddRole")]
        public async Task<IActionResult> AddRole([FromBody]AddRoleRequest model)
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
        [Route("UpdateRole")]
        public async Task<IActionResult> UpdateRole([FromBody]UpdateRoleRequest model)
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
        [Route("DeleteRole")]
        public async Task<IActionResult> DeleteRole([FromBody]DeleteRoleRequest model)
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
