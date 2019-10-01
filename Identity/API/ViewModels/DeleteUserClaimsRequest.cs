using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.ViewModels
{
    public class DeleteUserClaimsRequest
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        public List<ApplicationUserClaim> Claims { get; set; }
    }
}
