using Microsoft.EntityFrameworkCore;
using System.Reflection;
using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;

namespace YouTube.AspNetCore.Tutorial.Basic.Context
{
    public class CustomContext : DbContext
    {
        public CustomContext()
        {
        }

        public CustomContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ControllerName> ControllerNames { get; set; }
        public DbSet<Domain> Domains { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


    }
}
