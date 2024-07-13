using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;

namespace YouTube.AspNetCore.Tutorial.Basic.Configuration
{
    public class ControllerNameConfig : IEntityTypeConfiguration<ControllerName>
    {
        public void Configure(EntityTypeBuilder<ControllerName> builder)
        {
            builder.Navigation(x => x.Domains).AutoInclude();

            builder.HasData(new ControllerName()
            {
                Id = 1,
                Name = "Authenticate",
            }, new ControllerName()
            {
                Id = 2,
                Name = "Dashboard",
            }, new ControllerName()
            {
                Id = 3,
                Name = "ControllerName",
            }, new ControllerName()
            {
                Id = 4,
                Name = "Admin",
            }, new ControllerName()
            {
                Id = 5,
                Name = "Authenticated",
            }, new ControllerName()
            {
                Id = 6,
                Name = "Category",
            }, new ControllerName()
            {
                Id = 7,
                Name = "Domain",
            }, new ControllerName()
            {
                Id = 8,
                Name = "Exception",
            }, new ControllerName()
            {
                Id = 9,
                Name = "Product",
            }, new ControllerName()
            {
                Id = 10,
                Name = "Role",
            }, new ControllerName()
            {
                Id = 11,
                Name = "UserRole",
            });
        }
    }
}
