using System.ComponentModel.DataAnnotations;

namespace IS_ZJZ_B.Models
{
    public class Guidance
    {
            [Key]
            public int id { get; set; }
            public string place_of_departure { get; set; }
            public string mode_of_transportation { get; set; }
            public string disease_code { get; set; }
            public string status { get; set; }
            public int te_id { get; set; }
            public int hc_id { get; set; }

    }
}
