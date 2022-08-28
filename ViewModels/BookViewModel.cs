using DemoAppDevelopment.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DemoAppDevelopment.ViewModels
{
    public class BookViewModel
    {
        // test commit on git
        public Book book { set; get; }
        [Required(ErrorMessage = "Please choose profile image")]
        [Display(Name = "Profile Picture")]
        public IFormFile ProfileImage { get; set; }
    }
}
