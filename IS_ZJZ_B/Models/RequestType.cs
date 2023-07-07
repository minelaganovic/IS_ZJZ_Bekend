using System.ComponentModel.DataAnnotations;

namespace IS_ZJZ_B.Models
{
    public class RequestType
    {
        [Key]
        public int id { get; set; }
        public string tipzahteva { get; set; }
    }
}
