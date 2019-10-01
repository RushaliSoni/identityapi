
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

        public ApplicationRoleController(IRoleService  roleservice, UserManager<ApplicationUser> userManager)
        {
            _roleservice = roleservice;
            _userManager = userManager;
        }

        //AspNetUserRole Table CRUD OPeration

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
                    return Ok($"SuccessFully Set Role {roleName} For User {userName} in AspNetUserRoles Table");
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
                    return Ok($"SuccessFully Remove UserRole {roleName} For User {userName} in AspNetUserRole Table ");
                
            }

            return Ok("UnExpected Errors !!!");
        }
        [HttpPost]
        [Route("UpdateUserRole")]
        public async Task<IActionResult> UpdateUserRole([FromBody]UpdateUserRoleRequest model)
        {
            
            bool isExist = !string.IsNullOrEmpty(model.Email);
            IdentityResult Result;
            string roleName = string.Empty;

            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = await _userManager.FindByEmailAsync(model.Email);
                var UserRole = await _userManager.GetRolesAsync(applicationUser);
                if (UserRole != null)
                {
                    Result = await _roleservice.RemoveFromRoleAsync(applicationUser, model.Old_ApplicationRoleName);
                    string RoleName = applicationUser.Name;
                    Result = await _roleservice.AddToRoleAsync(applicationUser, model.New_ApplicationRoleName);


                    if (Result.Succeeded)
                    {
                       
                            return Ok(" SuccessFully Updated  UserRole In AspNetUserRole table...." + model.Email);
                        

                    }
                }
                Result = await _roleservice.AddToRoleAsync(applicationUser, model.Old_ApplicationRoleName);
            }


            return Ok($"Faild to Update Role {roleName} for user "+ model.Email);


        }

        // AspNetRoles table CRUD Operation

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
            bool isExist = !String.IsNullOrEmpty(model.RoleName);
            IdentityResult roleResult;


            if (ModelState.IsValid)
            {
                ApplicationRole applicationRole = await _roleservice.FindByNameAsync(model.RoleName);
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

            bool isExist = !String.IsNullOrEmpty(model.RoleName);
            IdentityResult roleResult;
            ApplicationRole applicationRole = await _roleservice.FindByNameAsync(model.RoleName);
            if (ModelState.IsValid)
            {
                if (isExist && _roleservice.FindByNameAsync(model.RoleName) != null)
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
