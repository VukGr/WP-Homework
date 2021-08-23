using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Back.Models
{
    public class Network
    {
        [Key]
        public int ID { get; set; }
        
        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(256)]
        [JsonIgnore]
        public string Password { get; set; }
        public virtual List<Channel> Channels { get; set; }
    }
}