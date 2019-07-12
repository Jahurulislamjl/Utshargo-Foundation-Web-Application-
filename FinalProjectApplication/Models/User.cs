using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

 


namespace FinalProjectApplication.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [Display(Name ="Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name ="UserName")]
        [System.Web.Mvc.Remote("IsUserNameExist", "Home", HttpMethod = "POST", ErrorMessage = "UserName already exist")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^\(?([0-9]{11})$", ErrorMessage = "Invalid Contact no")]
        [StringLength(11,ErrorMessage ="Invalid Contact no")]
        [System.Web.Mvc.Remote("IsPhoneExist", "Home", HttpMethod = "POST")]
        public string PhoneNo { get; set; }
        [Required]
        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public string DateOfBirth { get; set; }
        [Required]
        [Display(Name = "Email")]
        [System.Web.Mvc.Remote("IsEmailExist","Home",HttpMethod ="POST",ErrorMessage ="Email already exist")]
        //[RegularExpression("@",ErrorMessage ="Invalid Email")]
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be 8 digits")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Don't match Password")]
        [Display(Name = "Confirm Password")]
        public string ReTypePassword { get; set; }
        [Required]
        [Display(Name = "Rule")]
        public int Rule { get; set; }

        public bool VerificationStatus { get; set; }
        public int VerificationCode { get; set; }
    }
}