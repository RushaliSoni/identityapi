﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Identity.API.ViewModels;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Services
{
    public class ClaimService : IClaimService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public ClaimService(UserManager<ApplicationUser> userManager , RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IdentityResult> AddClaimAsync(ApplicationUser User, Claim claim)
        {
            return await _userManager.AddClaimAsync(User, claim);
        }

        public async Task<IdentityResult> AddClaimAsync(ApplicationRole role, Claim claim)
        {
            return await _roleManager.AddClaimAsync(role, claim);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string Email)
        {
            
            return await _userManager.FindByEmailAsync(Email);
        }

        public async Task<IList<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            return await _userManager.GetClaimsAsync(user);
        }

        public async Task<IList<Claim>> GetClaimsAsync(ApplicationRole user)
        {
            return await _roleManager.GetClaimsAsync(user);
        }

        public async Task<IdentityResult> RemoveClaimAsync(ApplicationUser User, Claim claim)
        {
            return await _userManager.RemoveClaimAsync(User, claim);
        }

        public async Task<IdentityResult> RemoveClaimAsync(ApplicationRole role, Claim claim)
        {
            return await _roleManager.RemoveClaimAsync(role, claim);
        }

        public async Task<IdentityResult> ReplaceClaimAsync(ApplicationUser User, Claim claim, Claim newclaim)
        {
            return await _userManager.ReplaceClaimAsync(User, claim,newclaim);
        }

        //public async Task<IdentityResult> ReplaceClaimAsync(ApplicationRole role, Claim claim, Claim newclaim)
        //{
        //    return await _roleManage
        //}
    }
}
