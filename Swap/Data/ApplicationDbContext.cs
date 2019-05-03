using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Swap.Models;

namespace Swap.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Swapped> Swapped { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        ApplicationUser user = new ApplicationUser
            {
                FirstName = "admin",
                LastName = "admin",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            modelBuilder.Entity<Item>().HasData(
            new Item()
            {
                Id = 1,
                UserId = user.Id,
                Category = "Clothing",
                Img = "https://www.bootbarn.com/dw/image/v2/BCCF_PRD/on/demandware.static/-/Sites-master-product-catalog-shp/default/dw7eaef6c3/images/648/2000232648_700_P1.JPG",
                Description = "Cool hat in good condition"
            },
            new Item()
            {
                Id = 2,
                UserId = user.Id,
                Category = "Home Appliances",
                Img = "https://images.crateandbarrel.com/is/image/Crate/EllaWhiteTableLampOffSHF15",
                Description = "Mildly good condition"
            },
             new Item()
             {
                 Id = 3,
                 UserId = user.Id,
                 Category = "Clothing",
                 Img = "https://cdn.shopify.com/s/files/1/0051/4802/products/i-octocat-code_600x600.png?v=1520399372",
                 Description = "Awesome shirt! good condition.. it just doesnt fit"
             },
             new Item()
             {
                 Id = 4,
                 UserId = user.Id,
                 Category = "Home Appliances",
                 Img = "https://www.westelm.com/weimgs/ab/images/wcm/products/201849/0247/folk-pad-printed-bowls-c.jpg",
                 Description = "Super awesome bowl set"
             }
        );
        }
        public DbSet<Swap.Models.Item> Item { get; set; }     
    }
}
