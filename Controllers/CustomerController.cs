using DemoAppDevelopment.Data;
using DemoAppDevelopment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DemoAppDevelopment.Controllers
{
    public class CustomerController : Controller
    {
        public ApplicationDbContext _context;
        public UserManager<ApplicationUser> _userManager;

        public CustomerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            List<Book> listBook = _context.Books.Include(b => b.Category).ToList();

            return View(listBook);
        }
    }
}
