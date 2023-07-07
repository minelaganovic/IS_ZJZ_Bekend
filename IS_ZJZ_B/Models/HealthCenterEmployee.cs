using System.ComponentModel.DataAnnotations;

namespace IS_ZJZ_B.Models
{
    public class HealthCenterEmployee
    {
        [Key]
        public int Id_hce { get; set; }

        /*        [StringLength(200)]*/
        public string name_hce { get; set; }

        public string city_hce { get; set; }
        public string flname_doctor { get; set; }
    }
}
