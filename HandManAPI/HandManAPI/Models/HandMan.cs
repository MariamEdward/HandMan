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
    public class HandMan
    {
        [Key]
        public string ID { get; set; }

        public bool Available { get; set; } //make it default true

        [Range(0.0, 5.0)]
        public float Rate { get; set; }

        [Required]
        public string Region { get; set; }

        [JsonIgnore]
        [ForeignKey("ID")]
        public IdentityUser User { get; set; }

        public int LocationId { get; set; }

        [JsonIgnore]
        [ForeignKey("LocationId ")]
        public Location Location { get; set; }

        public int ServiceId { get; set; }

        [JsonIgnore]
        [ForeignKey("ServiceId")]

        public Service Service { get; set; }

        [JsonIgnore]
        public ICollection<Transaction> Transactions { get; set; }
    }
} 