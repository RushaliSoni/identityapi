using Identity.API.ViewModels;
using Identity.Application.Services;
using Identity.Domain.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClaimManagementController : ControllerBase
    {
        private readonly IClaimService _claimService;
        private readonly IRoleService _roleService;

        public ClaimManagementController(IClaimService claimService , IRoleService roleService )
        {
            
            _claimService = claimService;
            _roleService = roleService;
        }
        //Here  it's for User claims CRUD Operation

        [HttpPost]
        [Route("AddUserClaims")]
        public async Task<IActionResult> AddUserClaims([FromBody]AddUserClaimsRequest addClaimsRequest)
        {
            ApplicationUser user = await _claimService.FindByEmailAsync(addClaimsRequest.Email);
            if (user != null)
            {
                foreach (ApplicationUserClaim claim in addClaimsRequest.Claims)
                {
                    await _claimService.AddClaimAsync(user, new Claim(claim.ClaimType, claim.ClaimValue));
                }

                return Ok("Claims SuccessFully Added For User " + addClaimsRequest.Email);
            }
            return Ok("Failed To Add Claims For User " + addClaimsRequest.Email);
        }

        [HttpPost]
        [Route("UpdateUserClaims")]
        public async Task<IActionResult> UpdateUserClaims([FromBody]UpdateUserClaimsRequest updateClaimsRequest)
        {
            bool isExist = !string.IsNullOrEmpty(updateClaimsRequest.Email);
            IdentityResult Result;
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = await _claimService.FindByEmailAsync(updateClaimsRequest.Email);
                var allClaims = await _claimService.GetClaimsAsync(applicationUser);
                var oldclaim = allClaims.FirstOrDefault(c => c.Type == updateClaimsRequest.OldType && c.Value == updateClaimsRequest.OldValue);
                if(oldclaim!=null)
                {
                    Result = await _claimService.ReplaceClaimAsync(applicationUser, new Claim(updateClaimsRequest.OldType, updateClaimsRequest.OldValue), new Claim(updateClaimsRequest.NewType, updateClaimsRequest.NewValue));

                    if (Result.Succeeded)
                    {
                        if (isExist)
                        {
                            return Ok("Claim SuccessFully Updated for user. ..." + updateClaimsRequest.Email);
                        }

                    }
                }
               
            }
            return Ok("UnExpected Error");
        }

        [HttpPost]
        [Route("DeleteUserClaims")]
        public async Task<IActionResult> DeleteUserClaims([FromBody]DeleteUserClaimsRequest deleteClaimsRequest)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = await _claimService.FindByEmailAsync(deleteClaimsRequest.Email);
                foreach (ApplicationUserClaim claim in deleteClaimsRequest.Claims)
                {
                    await _claimService.RemoveClaimAsync(applicationUser, new Claim(claim.ClaimType,claim.ClaimValue));
                };

                if (applicationUser == null)
                {
                    return Ok("User Not Found");
                }
                else
                {
                    return Ok("Claim Successfully Deleted for user..." + deleteClaimsRequest.Email);
                }
            }
            return Ok("UnExpected Error");

        }


        //Here it's CRUD operation for Role Claim

        [HttpPost]
        [Route("AddRoleClaims")]
        public async Task<IActionResult> AddRoleClaims([FromBody]AddRoleClaimsRequest addroleClaimsRequest)
        {
            ApplicationRole role = await _roleService.FindByNameAsync(addroleClaimsRequest.RoleName);
            if (role != null)
            {
                foreach (ApplicationRoleClaim claim in addroleClaimsRequest.Claims)
                {
                    await _claimService.AddClaimAsync(role, new Claim(claim.ClaimType, claim.ClaimValue));
                }

                return Ok("Claims SuccessFully Added For Role " + addroleClaimsRequest.RoleName);
            }
            return Ok("Failed To Add Claims For Role " + addroleClaimsRequest.RoleName);
        }


        [HttpPost]
        [Route("UpdateRoleClaims")]
        public async Task<IActionResult> UpdateRoleClaims([FromBody]UpdateRoleClaimsRequest updateRoleClaimsRequest)
        {
            bool isExist = !string.IsNullOrEmpty(updateRoleClaimsRequest.RoleName);
            if (ModelState.IsValid)
            {
                ApplicationRole applicationrole = await _roleService.FindByNameAsync(updateRoleClaimsRequest.RoleName);
                //var allClaims = await GetClaimsAsync(applicationrole);
                var getclaims = await _claimService.GetClaimsAsync(applicationrole);

                var claims = getclaims.FirstOrDefault(c => c.Type == updateRoleClaimsRequest.OldType && c.Value == updateRoleClaimsRequest.OldValue);
                if (claims != null)
                {
                    foreach (ApplicationRoleClaim claim in updateRoleClaimsRequest.Claims)
                    {
                        await _claimService.RemoveClaimAsync(applicationrole, new Claim(updateRoleClaimsRequest.OldType, updateRoleClaimsRequest.OldValue));
                        await _claimService.AddClaimAsync(applicationrole, new Claim(updateRoleClaimsRequest.NewType, updateRoleClaimsRequest.NewValue));

                    };
                   
                    return Ok("RoleClaim SuccessFully Updated...." + updateRoleClaimsRequest.RoleName);

                }
                foreach (ApplicationRoleClaim claim in updateRoleClaimsRequest.Claims)
                {
                    await _claimService.AddClaimAsync(applicationrole, new Claim(updateRoleClaimsRequest.NewType, updateRoleClaimsRequest.NewValue));
                }


            }
            return Ok("UnExpected Error");
        }


        [HttpPost]
        [Route("DeleteRoleClaims")]
        public async Task<IActionResult> DeleteRoleClaims([FromBody]DeleteRoleClaimsRequest deleteroleClaimsRequest)
        {
            if (ModelState.IsValid)
            {
                ApplicationRole role = await _roleService.FindByNameAsync(deleteroleClaimsRequest.RoleNmae);
                foreach (ApplicationRoleClaim claim in deleteroleClaimsRequest.Claims)
                {
                    await _claimService.RemoveClaimAsync(role, new Claim(claim.ClaimType, claim.ClaimValue));
                };

                if (role == null)
                {
                    return Ok("User Not Found");
                }
                else
                {
                    return Ok("RoleClaim Successfully Deleted..." + deleteroleClaimsRequest.RoleNmae);
                }
            }
            return Ok("UnExpected Error");

        }
    }
}
