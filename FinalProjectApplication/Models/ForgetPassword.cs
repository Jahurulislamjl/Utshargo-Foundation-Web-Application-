using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProjectApplication.Models
{
    public class ForgetPassword
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string UserName { get; set; }
       
        
    }
}