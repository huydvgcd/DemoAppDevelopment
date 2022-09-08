using DemoAppDevelopment.Data;
using DemoAppDevelopment.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System;
using DemoAppDevelopment.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DemoAppDevelopment.Enums;
using System.Threading.Tasks;
using DemoAppDevelopment.Utils;
using Microsoft.AspNetCore.Authorization;

namespace DemoAppDevelopment.Controllers
{   

    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        public IFormFile ProfileImage { get; set; }


        public BookController(ApplicationDbContext context)
        {
            _context = context;

        }

        //private string UploadedFile(BookViewModel model)
        //{
        //    string uniqueFileName = null;
        //    if (model.ProfileImage != null)
        //    {
        //        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            model.ProfileImage.CopyTo(fileStream);
        //        }
        //    }
        //    return uniqueFileName;
        //}

        [HttpGet]
        public IActionResult Index()
        {
            List<Book> listBook = _context.Books.Include(b => b.Category).ToList();
            return View(listBook);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var getCategoryInDb = (from category in _context.Categories
                                   where category.Status == CategoryStatus.Accepted
                                   select category).ToList();
            var model = new BookViewModel
            {
                listCategory = getCategoryInDb
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(BookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model = new BookViewModel
                {
                    listCategory = _context.Categories
                    .Where(c => c.Status == Enums.CategoryStatus.Accepted)
                    .ToList()
                };
                return View(model);
            }

            using (var memoryStream = new MemoryStream())
            {
                await model.ProfileImage.CopyToAsync(memoryStream);

                var newBook = new Book
                {
                    Author = model.book.Author,
                    Price = model.book.Price,
                    Title = model.book.Title,
                    Description = model.book.Description,
                    Quantity = model.book.Quantity,
                    CategoryId = model.book.CategoryId,
                    ProfilePicture = memoryStream.ToArray()
                };

                _context.Books.Add(newBook);
                await _context.SaveChangesAsync();

            }


            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            Book bookToDelete = _context.Books.Include(b => b.Category).FirstOrDefault(b => b.Id == id);
            if (bookToDelete == null)
            {
                return BadRequest();
            }

            _context.Remove(bookToDelete);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            Book book = _context.Books.Include(b => b.Category).FirstOrDefault(b => b.Id == id);


            //var bookInDb = _context.Books.Include(t => t.Category)
            //                .SingleOrDefault(t => t.Id == id);
            //if (bookInDb is null)
            //{
            //    return NotFound();
            //}

            ViewBag.ImageData = ConvertByteArrayToStringBase64(book.ProfilePicture);
            return View(book);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var getCategoryInDb = (from category in _context.Categories
                                   where category.Status == CategoryStatus.Accepted
                                   select category).ToList();
            var model = new BookViewModel
            {
                listCategory = getCategoryInDb
            };

            Book bookToUpdate = _context.Books.Include(b => b.Category).FirstOrDefault(b => b.Id == id);

            model.book = bookToUpdate;

            ViewBag.ImageData = ConvertByteArrayToStringBase64(bookToUpdate.ProfilePicture);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(BookViewModel model)
        {
            var bookUpdate = _context.Books.Include(b => b.Category).FirstOrDefault(b => b.Id == model.book.Id);
            if (!ModelState.IsValid)
            {
                var getCategoryInDb = (from category in _context.Categories
                                       where category.Status == CategoryStatus.Accepted
                                       select category).ToList();
                model.listCategory = getCategoryInDb;

                ViewBag.ImageData = ConvertByteArrayToStringBase64(bookUpdate.ProfilePicture);
                return View(model);
            }



            bookUpdate.Author = model.book.Author;
            bookUpdate.Price = model.book.Price;
            bookUpdate.Title = model.book.Title;
            bookUpdate.Description = model.book.Description;
            bookUpdate.Quantity = model.book.Quantity;
            bookUpdate.CategoryId = model.book.CategoryId;

            if (model.ProfileImage != null)
            {
                using (var memoryStream = new MemoryStream())

                {
                    await model.ProfileImage.CopyToAsync(memoryStream);

                    if (memoryStream != null)
                        bookUpdate.ProfilePicture = memoryStream.ToArray();
                }

            }
            await _context.SaveChangesAsync();


            return RedirectToAction("Index");
        }

        [NonAction]
        private string ConvertByteArrayToStringBase64(byte[] imageArray)
        {
            string imageBase64Data = Convert.ToBase64String(imageArray);

            return string.Format("data:image/jpg;base64, {0}", imageBase64Data);
        }
    }
}
