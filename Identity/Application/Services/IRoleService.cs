using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Application.Services
{
    public interface IRoleService
    {
        Task<ApplicationRole>  FindByIdAsync(string id);
        Task<ApplicationRole> FindByNameAsync(string ApplicationRolename);
        Task<IdentityResult> CreateAsync(ApplicationRole role);
        Task<IdentityResult> UpdateAsync(ApplicationRole role);
        Task<IdentityResult> DeleteAsync(ApplicationRole role);



    }
}
