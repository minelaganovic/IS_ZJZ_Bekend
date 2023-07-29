using System.ComponentModel.DataAnnotations;

namespace IS_ZJZ_B.Models
{
    public class TravelExpense
    {
        [Key]
        public int id { get; set; }
        public string date { get; set; }
        public int cost_price { get; set; }
        public string status { get; set; }
    }
}
