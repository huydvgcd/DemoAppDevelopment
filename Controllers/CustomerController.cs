using DemoAppDevelopment.Data;
using DemoAppDevelopment.Models;
using DemoAppDevelopment.Utils;
using DemoAppDevelopment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoAppDevelopment.Controllers
{
    [Authorize(Roles = Role.CUSTOMER)]
    public class CustomerController : Controller
    {
        public readonly ApplicationDbContext _context;
        public readonly UserManager<ApplicationUser> _userManager;

        public CustomerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {

            List<BookViewModel> listBookInHome = new List<BookViewModel>();
            var results = _context.Books.Include(t => t.Category).ToList();


            foreach (var item in results)
            {


                var model = new BookViewModel()
                {
                    Id = item.Id,
                    Title = item.Title,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    ImageUrl = ConvertByteArrayToStringBase64(item.ProfilePicture)
                };

                listBookInHome.Add(model);

            }
            return View(listBookInHome);
        }

        [NonAction]
        private string ConvertByteArrayToStringBase64(byte[] imageArray)
        {
            string imageBase64Data = Convert.ToBase64String(imageArray);

            return string.Format("data:image/jpg;base64, {0}", imageBase64Data);
        }

        public IActionResult Cart()
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var listItemInCart = (from item in _context.Carts where item.UserId == userId select item).Include(b => b.Book).ToList();

            return View(listItemInCart);
        }



        [HttpPost]
        public IActionResult SearchItem(string searchString)
        {
            var results = _context.Books.Include(c => c.Category)
                .Where(s => s.Title.Contains(searchString) || s.Category.CategoryName.Contains(searchString))
                .ToList();

            List<BookViewModel> listBookInHome = new List<BookViewModel>();

            foreach (var item in results)
            {


                var model = new BookViewModel()
                {
                    Id = item.Id,
                    Title = item.Title,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    ImageUrl = ConvertByteArrayToStringBase64(item.ProfilePicture)
                };

                listBookInHome.Add(model);

            }

            return View(nameof(Index), listBookInHome);
        }


        public IActionResult SearchItem()
        {
            return View(nameof(Index));
        }

    }
}
