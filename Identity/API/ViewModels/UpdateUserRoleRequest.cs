using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.ViewModels
{
    public class UpdateUserRoleRequest
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

       
        [Required(ErrorMessage = "ApplicationRoleName is Required.")]
        [Display(Name = "Old_RoleName")]
        public string Old_ApplicationRoleName { get; set; }
        [Required(ErrorMessage = "ApplicationRoleName is Required.")]
        [Display(Name = "New_RoleName")]
        public string New_ApplicationRoleName { get; set; }
    }
}
