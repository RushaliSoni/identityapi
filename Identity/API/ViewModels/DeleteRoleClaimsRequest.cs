using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.ViewModels
{
    public class DeleteRoleClaimsRequest
    {
        [Required]
        [Display(Name = "RoleName")]
        public string RoleNmae { get; set; }
        [Required]
        public List<ApplicationRoleClaim> Claims { get; set; }
    }
}
