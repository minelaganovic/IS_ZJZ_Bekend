using System.ComponentModel.DataAnnotations;

namespace IS_ZJZ_B.Models
{
    public class ExpenseTravel
    {
        [Key]
        public int id { get; set; }
        public int cost_price { get; set; }
        public string path { get; set; }
    }
}
