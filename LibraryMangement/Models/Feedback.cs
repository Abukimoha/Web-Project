using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryMangement.Models
{
    public class Feedback
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Message { get; set; }
    }
}