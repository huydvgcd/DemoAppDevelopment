using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using System.Collections.Generic;

namespace DemoAppDevelopment.Models
{
    [Table("Orders")]
    public class Orders
    {
        [Key]
        [Required]
        [Display(Name = "Id")]
        public int Id { set; get; }

        [Display(Name = "User Id")]
        public string UserId { set; get; }
        [ForeignKey("UserId")]
        public ApplicationUser AppUser { set; get; }

        public List<OrdersDetail> OrdersDetails { set; get; }

        [Display(Name = "Order Time")]
        public DateTime OrderTime { set; get; }




    }
}
