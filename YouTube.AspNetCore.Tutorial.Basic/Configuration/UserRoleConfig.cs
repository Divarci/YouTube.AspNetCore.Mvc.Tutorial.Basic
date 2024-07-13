using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;

namespace YouTube.AspNetCore.Tutorial.Basic.Configuration
{
    public class UserRoleConfig : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.Navigation(x => x.Role).AutoInclude();
            builder.Navigation(x => x.User).AutoInclude();

            builder.HasData(new UserRole
            {
                Id = 1,
                UserId = 1,
                RoleId =3
            }, new UserRole
            {
                Id = 2,
                UserId = 1,
                RoleId = 1
            }, new UserRole
            {
                Id = 3,
                UserId = 2,
                RoleId = 2
            }, new UserRole
            {
                Id = 4,
                UserId = 2,
                RoleId = 1
            }, new UserRole
            {
                Id = 5,
                UserId = 1,
                RoleId = 2
            });
        }
    }
}
