using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DemoAppDevelopment.Models
{
    [Table("Book")]
    public class Book
    {
        [Key]
        [Display(Name = "Key Book")]
        public int Id { set; get; }


        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }


        [Display(Name = "Book title")]
        public string Title { set; get; }


        [Display(Name = "Book price")]
        public int Price { set; get; }


        [Display(Name = "Book image")]
        public string ImgUrl { set; get; }

        [Display(Name = "Book Author")]
        public string Author { set; get; }

        [Display(Name = "Book Description")]
        public string Description { set; get; }

        [Display(Name = "Book Quantity")]
        public int Quantity { set; get; }

        public byte[] ProfilePicture { get; set; }
    }
}
