using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HandManAPI.Models
{
    public enum Status
    {
        Open=1,
        Closed=0,
        
    }
    public class Transaction
    {
        public int ID { get; set; }
        public Status Status { get; set; }
        public string Review { get; set; }
        public float CustomerRate { get; set; }
        public string CustomerId { get; set; }

        [JsonIgnore]
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        public string HandManId { get; set; }

        [JsonIgnore]
        [ForeignKey("HandManId")]
        public virtual HandMan HandMan { get; set; }

    } 
}