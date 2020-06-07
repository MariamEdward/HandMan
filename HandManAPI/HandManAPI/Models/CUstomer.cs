using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HandManAPI.Models
{
    public class Customer
    {
        [Key]
        public string ID { get; set; }
        
        [JsonIgnore]
        [ForeignKey("ID")]
        public IdentityUser User { get; set; }
        
        [JsonIgnore]
        public ICollection<Transaction> Transactions { get; set; }
    }
}