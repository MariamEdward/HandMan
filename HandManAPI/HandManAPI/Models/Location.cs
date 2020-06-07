using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HandManAPI.Models
{
    public class Location //conect to region ? //i thin yes
    {
        public int ID { get; set; }
        public float Latitude { get; set; }

        public float Longtide { get; set; }
        
        //[ForeignKey("HandMan")]
        //public string HandManId { get; set; }

        //public HandMan HandMan { get; set;  }

        //public String Regions { get; set; } 

    }
}