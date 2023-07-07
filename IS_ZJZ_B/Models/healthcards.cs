using System.ComponentModel.DataAnnotations;

namespace IS_ZJZ_B.Models
{
    public class healthcards
    {
        [Key]
        public int id { get; set; }
        public string lbo { get; set; }
        public string date_verification_hc { get; set; }
        public string date_expiration_hc { get; set; }
    }
}
