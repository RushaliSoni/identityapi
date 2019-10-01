using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.ViewModels
{
    public class UpdateRoleClaimsRequest
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }

        public string OldType { get; set; }
        public string NewType { get; set; }
        public string OldValue { get; set; }

        public string NewValue { get; set; }
        public List<ApplicationRoleClaim> Claims { get; set; }

    }
}
