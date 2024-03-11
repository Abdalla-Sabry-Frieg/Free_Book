using Domain.Entity;
using InfraStructure.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<IdentityUser>().ToTable("users", "Identity");
            //builder.Entity<IdentityRole>().ToTable("Roles", "Identity");
            //builder.Entity<IdentityUserRole<String>>().ToTable("UserRoles", "Identity");
            //builder.Entity<IdentityUserClaim<String>>().ToTable("UserClaim", "Identity");
            //builder.Entity<IdentityUserLogin<String>>().ToTable("UserLogin", "Identity");
            //builder.Entity<IdentityRoleClaim<String>>().ToTable("RoleClaim", "Identity");
            //builder.Entity<IdentityUserToken<String>>().ToTable("UserToken", "Identity");

            builder.Entity<Category>().Property(x => x.Id).HasDefaultValueSql("(newid())");
            builder.Entity<LogCategory>().Property(x => x.Id).HasDefaultValueSql("(newid())");
            builder.Entity<SubCategory>().Property(x => x.Id).HasDefaultValueSql("(newid())");
            builder.Entity<LogSubCategory>().Property(x => x.Id).HasDefaultValueSql("(newid())");
            builder.Entity<Book>().Property(x => x.Id).HasDefaultValueSql("(newid())");
            builder.Entity<LogBook>().Property(x => x.Id).HasDefaultValueSql("(newid())");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<LogCategory> LogCategories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<LogSubCategory> LogSubCategories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<LogBook> LogBooks { get; set; }
        
    }
}
