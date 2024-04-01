using Core.Entity;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class APILogin
    {
        public string username { get; set; }
        public string password { get; set; }
        public string DeviceID { get; set; }
        public string LoginMode { get; set; }
        public AccessMember AccessMember { get; set; }
        public string FCMToken { get; set; }
    }

    public class DynamicallyLevelName
    {
        //public string Language { get; set; }
        //public int Level { get; set; }
        //public string RankLabel { get; set; }
        //public string PositionLabel { get; set; }
        public string Language { get; set; }

        public int Level { get; set; }
        public string RankLabel { get; set; }
        public string PositionLabel { get; set; }
        public string PositionLabelList { get; set; }
        public string LevelType { get; set; }
        public string SinceLocal { get; set; }
        public string SinceGlobal { get; set; }
        public string ShowLocation { get; set; }
        public string ShowLocation2 { get; set; }
    }
}