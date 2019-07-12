using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProjectApplication.Models
{
    public class Login
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Email")]
        //[RegularExpression("@",ErrorMessage ="Invalid Email")]
        //[EmailAddress(ErrorMessage = "Invalid Email")]
        [System.Web.Mvc.Remote("IsUserVerify", "Home", HttpMethod = "POST", ErrorMessage = "Your account is not verified.")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int Rule { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        //[MaxLength(11,ErrorMessage ="Invalid Phone no")]
        //[MinLength(9,ErrorMessage ="Invalid phone no")]
        //[RegularExpression(@"^\(?([0-10]{3})\)?[-. ]?([0-10]{3})[-. ]?([0-10]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        //[RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong mobile")]
        public string PhoneNo { get; set; }
        public bool VerificationStatus { get; set; }
        public int VerificationCode { get; set; }
    }
}