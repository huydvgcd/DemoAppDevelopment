using DemoAppDevelopment.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DemoAppDevelopment.ViewModels
{
    public class AdminViewModel
    {
        public List<ApplicationUser> Users { set; get; } = new List<ApplicationUser>();
        [BindProperty]
        public InputModel Input { get; set; }
        public SelectList RoleSelectList { get; set; }


        public class InputModel
        {
            public string Role { get; set; }
        }
    }
}
