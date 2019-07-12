using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProjectApplication.Models
{
    public class Blood
    {
        [Key]
        public int BloodId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string RequiredBloodGroup { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string Date { get; set; }
      
        [Required]
        [DataType(DataType.Time)]
        public string Time { get; set; }
        [Required]
        [RegularExpression(@"^\(?([0-9]{11})$", ErrorMessage = "Invalid Contact no")]
        [StringLength(11, ErrorMessage = "Invalid Contact no")]
        [System.Web.Mvc.Remote("CheckPhone", "Blood", HttpMethod = "POST")]
        public string PhoneNo { get; set; }
       
    }
}