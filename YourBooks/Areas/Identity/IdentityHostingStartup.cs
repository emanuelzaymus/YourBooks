﻿using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourBooks.Models;

[assembly: HostingStartup(typeof(YourBooks.Areas.Identity.IdentityHostingStartup))]
namespace YourBooks.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<YourBooksContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("YourBooksContextConnection")));

                services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<YourBooksContext>();
            });
        }
    }
}