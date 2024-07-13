using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;

namespace YouTube.AspNetCore.Tutorial.Basic.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Navigation(x => x.UserRoles).AutoInclude();

            var admin = new User
            {
                Id = 1,
                Email = "testadmin@gmail.com",
                Fullname = "TestAdmin",
            };
            admin.PasswordHash = PassHasher(admin, "password12*");

            var member = new User
            {
                Id = 2,
                Email = "testmember@gmail.com",
                Fullname = "TestMember",
            };
            member.PasswordHash = PassHasher(member, "password12*");
            builder.HasData(admin,member);   
        }

        private string PassHasher(User user, string password)
        {
            var passHasher = new PasswordHasher<User>();
            return passHasher.HashPassword(user, password);           
        }
    }
}
