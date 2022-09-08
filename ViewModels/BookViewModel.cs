using DemoAppDevelopment.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DemoAppDevelopment.ViewModels
{
    public class BookViewModel
    {
        public Book book { set; get; }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public int Price { get; set; }
        public int Quantity { set; get; }

        public Category Category { get; set; }

        public List<Category> listCategory { set; get; }

        [Required(ErrorMessage = "Please choose profile image")]
        [Display(Name = "Profile Picture")]
        public IFormFile ProfileImage { get; set; }
        public string ImageUrl { get; set; }
    }
}