using DemoAppDevelopment.Data;
using DemoAppDevelopment.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System;

namespace DemoAppDevelopment.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        public IFormFile ProfileImage { get; set; }
        private readonly IWebHostEnvironment webHostEnvironment;

        public BookController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        private string UploadedFile(BookViewModel model)
        {
            string uniqueFileName = null;
            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Book> listBook = _context.Books.Include(b => b.Category).ToList();
            return View(listBook);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var listCategories = _context.Categories.ToList();
            ViewData["listCategories"] = listCategories;
            return View();
        }

        [HttpPost]
        public IActionResult Create(BookViewModel model)
        {
            string uniqueFileName = UploadedFile(model);
            Book newBook = new Book
            {
                Author = model.book.Author,
                Price = model.book.Price,
                Title = model.book.Title,
                Description = model.book.Description,
                Quantity = model.book.Quantity,
                CategoryId = model.book.CategoryId,
                ProfilePicture = uniqueFileName
            };

            _context.Books.Add(newBook);
            _context.SaveChanges();
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
            return View(book);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var listCategories = _context.Categories.ToList();
            ViewData["listCategories"] = listCategories;

            Book book = _context.Books.Include(b => b.Category).FirstOrDefault(b => b.Id == id);
            return View(book);
        }

        [HttpPost]
        public IActionResult Update(Book book)
        {
            var bookUpdate = _context.Books.Include(b => b.Category).FirstOrDefault(b => b.Id == book.Id);

            bookUpdate.Author = book.Author;
            bookUpdate.Price = book.Price;
            bookUpdate.Title = book.Title;
            bookUpdate.Description = book.Description;
            bookUpdate.Quantity = book.Quantity;
            bookUpdate.CategoryId = book.CategoryId;




            _context.Books.Update(bookUpdate);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
