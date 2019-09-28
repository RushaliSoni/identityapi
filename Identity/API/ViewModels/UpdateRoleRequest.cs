using System.ComponentModel.DataAnnotations;

namespace Identity.API.ViewModels
{
    public class UpdateRoleRequest
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Role is Required.")]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        //[Required(ErrorMessage = "Operation Type is Required.")]
        //public EnAddOrRemoveRole AddOrUpdateorDeleteRole { set; get; }

        [Required(ErrorMessage = "Description is Required.")]
        public string Description { get; set; }
    }

    //public enum EnAddOrRemoveRole
    //{
    //    Delete = 0,
    //    Add = 1,
    //    update=2
        
    //}
}
