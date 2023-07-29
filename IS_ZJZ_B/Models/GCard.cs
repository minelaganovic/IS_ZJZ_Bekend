using System.ComponentModel.DataAnnotations;

namespace IS_ZJZ_B.Models
{
    public class GCard
    {
        [Key]
        public int id { get; set; }
        public string place_of_departure{ get; set; }
        public string mode_of_transportation{ get; set; }
        public string disease_code { get; set; }
        public string status { get; set; }
        public int id_te { get; set; }
        public int id_hc { get; set; }

    }
}
