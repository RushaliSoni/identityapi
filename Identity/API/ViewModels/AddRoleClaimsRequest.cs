using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.ViewModels
{
    public class AddRoleClaimsRequest
    {
        [Required]
        [Display(Name = "RoleName")]
        public string RoleName { get; set; }

        [Required]
        public List<ApplicationRoleClaim> Claims { get; set; }

    }
    public class ApplicationRoleClaim
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
