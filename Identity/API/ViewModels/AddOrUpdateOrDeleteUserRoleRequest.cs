using System.ComponentModel.DataAnnotations;

namespace Identity.API.ViewModels
{
    public class AddOrUpdateOrDeleteUserRoleRequest
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Role is Required.")]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [Required(ErrorMessage = "Operation Type is Required.")]
        public EnAddOrRemoveRole AddOrRemoveRole { set; get; }

        [Required(ErrorMessage = "Description is Required.")]
        public string Description { get; set; }
    }

    public enum EnAddOrRemoveRole
    {
        AddOrUpdate = 0,
        Delete = 1
    }
}
