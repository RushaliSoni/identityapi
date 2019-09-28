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

        public ClaimManagementController(IClaimService claimService)
        {
            
            _claimService = claimService;
        }

        [HttpPost]
        [Route("AddUserClaims")]
        public async Task<IActionResult> AddUserClaims([FromBody]AddClaimsRequest addClaimsRequest)
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
        public async Task<IActionResult> UpdateUserClaims([FromBody]UpdateClaimsRequest updateClaimsRequest)
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
                            return Ok("Claim SuccessFully Updated...." + updateClaimsRequest.Email);
                        }

                    }
                }
               
            }
            return Ok("UnExpected Error");
        }

        [HttpPost]
        [Route("DeleteUserClaims")]
        public async Task<IActionResult> DeleteUserClaims([FromBody]DeleteClaimsRequest deleteClaimsRequest)
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
                    return Ok("Claim Successfully Deleted..." + deleteClaimsRequest.Email);
                }
            }
            return Ok("UnExpected Error");

        }

    }
}
