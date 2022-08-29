using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DemoAppDevelopment.Models
{
    [Table("Orders Detail")]
    public class OrdersDetail
    {

        [Display(Name = "Order Id")]
        public int? OrderId { set; get; }

        [ForeignKey("OrderId")]
        public Orders Orders { set; get; }

        [Display(Name = "Book Id")]
        public int? BookId { set; get; }
        public Book Book { set; get; }


        [Display(Name = "Quantity")]
        [Range(1, 100)]
        public int Quantity { set; get; }

        [Display(Name = "Total")]
        public int Total { set; get; }
    }
}
