using System.ComponentModel.DataAnnotations;

namespace Back.Models
{
    public class Slot
    {
        [Key]
        public int ID { get; set; }

        [Range(0, 23)]
        public int Hour { get; set; }

        [MaxLength(256)]
        public string Content { get; set; }

        [MaxLength(256)]
        public string Color { get; set; }
    }
}