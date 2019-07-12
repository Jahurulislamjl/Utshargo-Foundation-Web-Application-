using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProjectApplication.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
       
        public int UserId { get; set; }
        [Required]
        [Display(Name ="Title")]
        public string EventTitle { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string EventDescription { get; set; }
        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public string Date { get; set; }
        [Required]
        [Display(Name = "From")]
        [DataType(DataType.Time)]
        public string FromTime { get; set; }
        [Required]
        [Display(Name = "To")]
        [DataType(DataType.Time)]
        public string ToTime { get; set; }
        [Required]
        [Display(Name = "Location")]
        public string Location { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Created Time")]
        public string EventCreateTime { get; set; }


    }
}