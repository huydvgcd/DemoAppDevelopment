using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using DemoAppDevelopment.Enums;

namespace DemoAppDevelopment.Controllers
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int Id { set; get; }


        public string CategoryName { set; get; }
        public DateTime CreateAt { set; get; }
        public CategoryStatus Status { set; get; }

        public List<Book> Books { get; set; }
    }
}
