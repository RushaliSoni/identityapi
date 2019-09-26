using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.API.ViewModels
{
    public class AddClaimsRequest
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        public List<ApplicationUserClaim> Claims { get; set; }  
    }

    public class ApplicationUserClaim
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }

}
