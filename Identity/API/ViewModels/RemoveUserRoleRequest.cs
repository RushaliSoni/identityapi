using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.ViewModels
{
    public class RemoveUserRoleRequest
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "ApplicationRoleName is Required.")]
        [Display(Name = "RoleName")]
        public string ApplicationRoleName { get; set; }
    }
}
