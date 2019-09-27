using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.ViewModels
{
    public class AddUserRoleRequest
    {
        //public string Id { get; set; }

        [Required(ErrorMessage = "Role is Required.")]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        //[Required(ErrorMessage = "Operation Type is Required.")]
        //public EnAddOrRemoveRole AddOrUpdateorDeleteRole { set; get; }

        [Required(ErrorMessage = "Description is Required.")]
        public string Description { get; set; }
    }
}
