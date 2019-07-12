using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProjectApplication.Models
{
    public class Notification
    {
        [Key]
        
        public int NotificationId { get; set; }
       
        public string  NotificationTitle { get; set; }
        public string  NotificationDescription { get; set; }
        public string NotificationDate { get; set; }
        
    }
}