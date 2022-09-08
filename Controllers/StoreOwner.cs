using DemoAppDevelopment.Data;
using DemoAppDevelopment.Models;
using DemoAppDevelopment.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAppDevelopment.Controllers
{
    
    public class StoreOwnerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public StoreOwnerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        [Authorize(Roles = Role.STORE_OWNER)] 
        [HttpGet]
        public async Task<IActionResult> IndexAsync(List<ApplicationUser> listCustomer)
        {
            if (listCustomer.Count != 0)
            {
                return View(listCustomer);
            }
            var usersInRole = await _userManager.GetUsersInRoleAsync(Role.CUSTOMER);
            return View(usersInRole);
        }

        [Authorize(Roles = Role.STORE_OWNER)] 
        [HttpPost]
        public async Task<IActionResult> IndexAsync(string searchString)
        {

            if (searchString == null)
            {
                return NotFound();
            }
            return await SearchCustomerAsync(searchString);


        }
        
        [Authorize(Roles = Role.STORE_OWNER)] 
        [NonAction]
        public async Task<IActionResult> SearchCustomerAsync(string searchString)
        {
            var customer = await _userManager.GetUsersInRoleAsync(Role.CUSTOMER);

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var result = customer.Where(t => t.Email.Contains(searchString)).ToList();

                return View(nameof(Index), result);
            }

            return View("Index");
        }

        [Authorize(Roles = Role.STORE_OWNER)] 
        [HttpGet]
        public IActionResult SendCategoryRequest()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = Role.STORE_OWNER)] 
        public IActionResult SendCategoryRequest(Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }

            Category newCategory = new Category();

            newCategory.CategoryName = category.CategoryName;
            newCategory.Status = Enums.CategoryStatus.InProgess;

            _context.Add(newCategory);
            _context.SaveChanges();
            TempData["Message"] = "Send Category Successfully";

            return View(newCategory);
        }



        [HttpGet]
        [Authorize(Roles = Role.STORE_OWNER)] 
        public IActionResult ListCustomerOrder()
        {

            // var listOrder = _context.OrdersDetails.Include(b => b.Orders).ThenInclude(u => u.AppUser).ToList();

            var order = _context.Orders.Include(u => u.AppUser).Include(b => b.OrdersDetails).ToList();
            return View(order);
        }
        
        [Authorize(Roles = Role.CUSTOMER+ ","+ Role.STORE_OWNER)]
        public IActionResult OrderCustomerDetail(int orderId)
        {
            var orderDetail = (from o in _context.OrdersDetails where o.OrderId == orderId select o)
                .Include(b => b.Book).ThenInclude(c => c.Category).ToList();

            return View(orderDetail);
        }

    }
}
