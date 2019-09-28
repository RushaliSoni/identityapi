using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleService(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role)
        {
            return await userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> CreateAsync(ApplicationRole role)
        {
            IdentityResult roleResult= await roleManager.CreateAsync(role);
            return roleResult; 
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationRole role)
        {
            return await roleManager.DeleteAsync(role);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationRole> FindByIdAsync(string id)
        {
            return await roleManager.FindByIdAsync(id);
               
        }

        public async Task<ApplicationRole> FindByNameAsync(string ApplicationRolename)
        {
            return await roleManager.FindByNameAsync(ApplicationRolename);
        }

        public async Task<IdentityResult> RemoveFromRoleAsync(ApplicationUser user, string role)
        {
            return await userManager.RemoveFromRoleAsync(user, role);

        }

        public async Task<IdentityResult> UpdateAsync(ApplicationRole role)
        {
            IdentityResult roleResult = await roleManager.UpdateAsync(role);
            return roleResult;
        }
    }
}
