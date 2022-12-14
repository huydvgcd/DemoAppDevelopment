using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DemoAppDevelopment.Models
{
    public class Cart
    {
        public string UserId { set; get; }
        public ApplicationUser AppUser { set; get; }


        [Display(Name = "Book Id")]
        public int? BookId { set; get; }


        [ForeignKey("BookId")]
        public Book Book { set; get; }

        [Range(1, 100)]
        public int Quantity { set; get; }

        public int Total { set; get; }
    }
}
