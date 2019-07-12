using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProjectApplication.Models
{
    public class ResetPassword
    {
        [Display(Name ="Enter Password")]
        [System.Web.Mvc.Remote("IsOldPasswordMatch", "Home", HttpMethod = "POST", ErrorMessage = "Invalid password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Display(Name = "Enter New Password")]
        [MinLength(8, ErrorMessage = "Password must be 8 digits")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage ="Password do not match")]
        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}