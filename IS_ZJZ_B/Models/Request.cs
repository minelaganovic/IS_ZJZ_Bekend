using System.ComponentModel.DataAnnotations;

namespace IS_ZJZ_B.Models
{
    public class Request
    {
        [Key]
        public int id { get; set; }
        public int userid{ get; set; }
        public string document { get; set; }
        public int type_id { get; set; }
    }
}
