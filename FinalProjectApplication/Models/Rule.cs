using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProjectApplication.Models
{
    public class Rule
    {
        [Key]
        public int Id { get; set; }
        public string RuleName  { get; set; }
    }
}