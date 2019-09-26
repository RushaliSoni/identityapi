using Identity.API.ViewModels;
using Identity.Domain.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClaimManagementController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ClaimManagementController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        
        [HttpPost]
        [Route("AddUserClaims")]
        public async Task<IActionResult> AddUserClaims([FromBody]AddClaimsRequest addClaimsRequest)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(addClaimsRequest.Email);
            if (user != null)
            {
                foreach (ApplicationUserClaim claim in addClaimsRequest.Claims)
                {
                    await _userManager.AddClaimAsync(user,new Claim(claim.ClaimType,claim.ClaimValue));
                }

                return Ok("Claims SuccessFully Added For User " + addClaimsRequest.Email);
            }
            return Ok("Failed To Add Claims For User " + addClaimsRequest.Email);
        }
    }
}