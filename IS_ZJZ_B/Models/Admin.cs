using System.ComponentModel.DataAnnotations;

namespace IS_ZJZ_B.Models
{
    public class Admin
    {
        [Key]
        public int id { get; set; }
        public string email { get; set; }
        public string pwd { get; set; }
        public string Role { get; set; }

        public string Token { get; set; }
    }
}
