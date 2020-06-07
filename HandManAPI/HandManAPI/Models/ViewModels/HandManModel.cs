using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HandManAPI.Models
{
    public class HandManModel
    {
        public string ID { get; set; }
 
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Region { get; set; }
        public float Latitude { get; set; }

        public float Longtide { get; set; }
       
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public List<string> Reviews { get; set; }
        public float Rate { get; set; }
    }
}