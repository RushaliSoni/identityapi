using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.ViewModels
{
    public class UpdateClaimsRequest
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string OldType { get; set; }
        public string NewType { get; set; }
        public string OldValue { get; set; }

        public string NewValue { get; set; }

       
        //public List<ApplicationUserClaim> Claims { get; set; }
    }
}
