using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace HandManAPI.Models
{
    [RoutePrefix("api/services")]
    public class Service
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        public string Image { get; set; }
        //not mapped into db so it is not useful
        [JsonIgnore]
        public  ICollection<HandMan> SubscribedHandMen { get; set; }
    }
}