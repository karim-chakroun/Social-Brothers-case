using Microsoft.Build.Framework;

namespace Social_Brothers_case.Models
{
    public class adress
    {
        public int id { get; set; }
        [Required]
        public string street { get; set; }
        [Required]
        public double houseNumber { get; set; }
        [Required]
        public string zipCode { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string country { get; set; }
    }
}
