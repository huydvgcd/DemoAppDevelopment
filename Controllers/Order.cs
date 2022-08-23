using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;

namespace DemoAppDevelopment.Controllers
{
    [Table("Orders")]
    public class Orders
    {
        [Key]
        [Required]
        [Display(Name = "Id")]
        public int Id { set; get; }

        [ForeignKey("UserId")]
        public ApplicationUser AppUser { set; get; }


        [Display(Name = "Order Time")]
        public DateTime OrderTime { set; get; }



    }
}
