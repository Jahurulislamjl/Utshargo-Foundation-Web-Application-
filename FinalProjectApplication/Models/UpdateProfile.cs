using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProjectApplication.Models
{
    public class UpdateProfile
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "UserName")]
        [System.Web.Mvc.Remote("CheckUserNameExistForUpdateProfile", "Home", HttpMethod = "POST", ErrorMessage = "UserName already exist")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber,ErrorMessage ="Invalid phone number")]
        [System.Web.Mvc.Remote("CheckPhoneExistForUpdateProfile", "Home", HttpMethod = "POST")]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^\(?([0-9]{11})$", ErrorMessage = "Invalid Contact no")]
        [StringLength(11,ErrorMessage ="Invalid Phone number")]
        public string PhoneNo { get; set; }
        [Required(ErrorMessage ="This Field is required")]
        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }
        [Display(Name = "New Blood Group")]
        public string NewBloodGroup { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Email")]
        [System.Web.Mvc.Remote("CheckEmailExistForUpdateProfile", "Home", HttpMethod = "POST", ErrorMessage = "Email already exist")]
        //[RegularExpression("@",ErrorMessage ="Invalid Email")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Code is required")]
        public int VerificationCode { get; set; }
    }
}