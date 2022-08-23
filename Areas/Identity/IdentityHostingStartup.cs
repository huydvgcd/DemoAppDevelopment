//using System;
//using DemoAppDevelopment.Areas.Identity.Data;
//using DemoAppDevelopment.Data;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

//[assembly: HostingStartup(typeof(DemoAppDevelopment.Areas.Identity.IdentityHostingStartup))]
//namespace DemoAppDevelopment.Areas.Identity
//{
//    public class IdentityHostingStartup : IHostingStartup
//    {
//        public void Configure(IWebHostBuilder builder)
//        {
//            builder.ConfigureServices((context, services) => {
//                services.AddDbContext<DemoAppDevelopmentContext>(options =>
//                    options.UseSqlServer(
//                        context.Configuration.GetConnectionString("DemoAppDevelopmentContextConnection")));

//                services.AddDefaultIdentity<DemoAppDevelopmentUser>(options => options.SignIn.RequireConfirmedAccount = true)
//                    .AddEntityFrameworkStores<DemoAppDevelopmentContext>();
//            });
//        }
//    }
//}