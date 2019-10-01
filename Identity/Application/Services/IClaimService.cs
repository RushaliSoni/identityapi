using Identity.API.ViewModels;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.Application.Services
{
    public interface IClaimService
    {
        Task<ApplicationUser> FindByEmailAsync(string Email);
        Task<IList<Claim>> GetClaimsAsync(ApplicationUser user);
        Task<IList<Claim>> GetClaimsAsync(ApplicationRole user);
        Task<IdentityResult> AddClaimAsync(ApplicationUser User , Claim claim);
        Task<IdentityResult> AddClaimAsync(ApplicationRole role , Claim claim);
        Task<IdentityResult> RemoveClaimAsync(ApplicationUser User , Claim claim);
        Task<IdentityResult> RemoveClaimAsync(ApplicationRole role , Claim claim);
        Task<IdentityResult> ReplaceClaimAsync(ApplicationUser User , Claim claim , Claim newclaim);
        //Task<IdentityResult> ReplaceClaimAsync(ApplicationRole role, Claim claim, Claim newclaim);

    }
}
