using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;

namespace YouTube.AspNetCore.Tutorial.Basic.Configuration
{
    public class DomainConfig : IEntityTypeConfiguration<Domain>
    {
        public void Configure(EntityTypeBuilder<Domain> builder)
        {
            builder.Navigation(x => x.Role).AutoInclude();
            builder.Navigation(x => x.ControllerName).AutoInclude();

            builder.HasData(new Domain()
            {
                Id = 1,
                ControllerNameId = 1,
                RoleId = 1,
            },
            new Domain()
            {
                Id = 2,
                ControllerNameId = 2,
                RoleId = 2,
            }, 
            new Domain()
            {
                Id = 3,
                ControllerNameId = 3,
                RoleId = 3,
            },
            new Domain()
            {
                Id = 4,
                ControllerNameId = 8,
                RoleId = 1,
            }, 
            new Domain()
            {
                Id = 5,
                ControllerNameId = 4,
                RoleId = 3,
            }, 
            new Domain()
            {
                Id = 6,
                ControllerNameId = 5,
                RoleId = 3,
            }, 
            new Domain()
            {
                Id = 7,
                ControllerNameId = 6,
                RoleId = 3,
            }, 
            new Domain()
            {
                Id = 8,
                ControllerNameId = 7,
                RoleId = 3,
            }, 
            new Domain()
            {
                Id = 9,
                ControllerNameId = 10,
                RoleId = 3,
            }, 
            new Domain()
            {
                Id = 10,
                ControllerNameId = 11,
                RoleId = 3,
            }, 
            new Domain()
            {
                Id = 11,
                ControllerNameId = 2,
                RoleId = 3,
            }, 
            new Domain()
            {
                Id = 12,
                ControllerNameId = 9,
                RoleId = 2,
            }, 
            new Domain()
            {
                Id = 13,
                ControllerNameId = 5,
                RoleId = 2,
            }, 
            new Domain()
            {
                Id = 14,
                ControllerNameId = 6,
                RoleId = 2,
            }, 
            new Domain()
            {
                Id = 15,
                ControllerNameId = 9,
                RoleId = 1,
            });
        }
    }
}
