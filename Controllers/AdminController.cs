using DemoAppDevelopment.Data;
using DemoAppDevelopment.Models;
using DemoAppDevelopment.Utils;
using DemoAppDevelopment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace DemoAppDevelopment.Controllers
{
    [Authorize(Roles = Role.ADMIN)]
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public SelectList RoleSelectList { get; set; } = new SelectList(new List<string>
          {
            "All User",
            Role.STORE_OWNER,
            Role.CUSTOMER
          }
        );
        public AdminController(ApplicationDbContext context,
                                UserManager<ApplicationUser> userManager,
                                RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        [HttpGet]
        public IActionResult Index()
        {


            var model = new AdminViewModel();

            foreach (var user in _userManager.Users)
            {
                //if (!await _userManager.IsInRoleAsync(user, Role.ADMIN))
                //{
                //    model.Users.Add(user);
                //}

                model.Users.Add(user);

            }

            model.RoleSelectList = RoleSelectList;


            return View(model);
        }


        [HttpPost]
        public IActionResult Index(AdminViewModel adminViewModel)
        {
            var adminUser = new AdminViewModel();

            var roleSelectedInView = adminViewModel.Input.Role;

            if (roleSelectedInView == Role.STORE_OWNER)
            {
                adminUser = GetUserByRole(Role.STORE_OWNER);
            }
            else if (roleSelectedInView == Role.CUSTOMER)
            {
                adminUser = GetUserByRole(Role.CUSTOMER);
            }
            else if (roleSelectedInView == Role.ADMIN)
            {
                adminUser = GetUserByRole(Role.ADMIN);
            }
            else
            {
                adminUser = new AdminViewModel();

                foreach (var user in _userManager.Users)
                {

                    adminUser.Users.Add(user);

                }
            }

            adminUser.RoleSelectList = RoleSelectList;
            return View(adminUser);
        }

        public IActionResult ListRoleUser(string selectRole)
        {
            var adminUser = new AdminViewModel();
            if (selectRole == Role.CUSTOMER)
            {
                adminUser = GetUserByRole(Role.CUSTOMER);
            }

            if (selectRole == Role.STORE_OWNER)
            {
                adminUser = GetUserByRole(Role.STORE_OWNER);
            }
            return View(nameof(ListRoleUser), adminUser);
        }


        [HttpGet]
        public IActionResult ChangePassword(string id)
        {
            var getUser = _context.Users.SingleOrDefault(t => t.Id == id);



            return View(getUser);
        }

        [HttpPost]
        public IActionResult ChangePassword(string id, [Bind("PasswordHash")] ApplicationUser user)
        {
            var getUser = _context.Users.SingleOrDefault(t => t.Id == id);
            var newPassword = user.PasswordHash;

            if (getUser == null)
            {
                return BadRequest();
            }
            if (newPassword == null)
            {
                ModelState.AddModelError("NoInput", "You have not input new password.");
                return View(getUser);
            }

            getUser.PasswordHash = _userManager.PasswordHasher.HashPassword(getUser, newPassword);
            TempData["Message"] = "Update Successfully";



            _context.SaveChanges();
            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult ListCategoriesInProgress()
        {
            var categories = _context.Categories
                .Where(t => t.Status == Enums.CategoryStatus.InProgess)
                .ToList();

            return View("VerifyCategoryRequest", categories);
        }

        [HttpGet]
        public IActionResult VerifyCategoryRequest(string name, int id)
        {

            var listCategory = from Category in _context.Categories select Category;
            var categoryAfterUpdate = _context.Categories.SingleOrDefault(c => c.Id == id);

            if (name == "accept")
            {
                AcceptCategoryRequest(id);

            }
            if (name == "reject")

            {
                RejectCategoryRequest(id);
            }

            return RedirectToAction(nameof(ListCategoriesInProgress));
        }



        [HttpGet]
        public IActionResult AcceptCategoryRequest(int id)
        {
            var categoryVerify = _context.Categories.SingleOrDefault(c => c.Id == id);

            if (categoryVerify == null)
            {
                return BadRequest();
            }

            categoryVerify.Status = Enums.CategoryStatus.Accepted;
            _context.SaveChanges();

            return RedirectToAction("VerifyCategoryRequest");
        }

        [HttpGet]
        public IActionResult RejectCategoryRequest(int id)
        {
            var categoryVerify = _context.Categories.SingleOrDefault(c => c.Id == id);

            if (categoryVerify == null)
            {
                return BadRequest();
            }
            
            categoryVerify.Status = Enums.CategoryStatus.Rejected;
            _context.SaveChanges();

            return RedirectToAction("VerifyCategoryRequest");
        }




        [NonAction]
        private AdminViewModel GetUserByRole(string role)
        {
            var adminUser = new AdminViewModel()
            {
                Users = (List<ApplicationUser>)_userManager.GetUsersInRoleAsync(role).Result
            };
            return adminUser;
        }



    }
}
