using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProjectApplication.Models
{
    public class Image
    {
        
        [Key]
        public int Id { get; set; }
        [Display(Name ="Upload Image")]
        public string Title { get; set; }
        public string ImagePath { get; set; }

    }
}