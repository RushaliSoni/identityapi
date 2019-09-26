using System.ComponentModel.DataAnnotations;

namespace Identity.API.ViewModels
{
    public class SetOrRemoveUserRoleRequest
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Operation Type is Required.")]
        public EnSetRemoveRole SetOrRemove { get; set; }

        [Required(ErrorMessage = "ApplicationRoleName is Required.")]
        [Display(Name = "RoleName")]
        public string ApplicationRoleName { get; set; }
    }

    public enum EnSetRemoveRole
    {
        Set = 0,
        Remove = 1
    }
}
