using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DemoAppDevelopment.Controllers
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string HomeAddress { get; set; }

        public virtual List<Cart> Carts { get; set; }
    }
}
