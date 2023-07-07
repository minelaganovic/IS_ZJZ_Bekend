using System.ComponentModel.DataAnnotations;

namespace IS_ZJZ_B.Models
{
    public class User
    {

        [Key]
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string  address { get; set; }
        public string jmbg { get; set; }
        public string email { get; set; }
        public string pwd { get; set; }
        public string date { get; set; }
        public string place { get; set; }
        public string status { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

    }
}
