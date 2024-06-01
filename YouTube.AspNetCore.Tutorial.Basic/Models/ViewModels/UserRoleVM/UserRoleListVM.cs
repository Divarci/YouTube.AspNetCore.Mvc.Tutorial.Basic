using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.RoleVM;

namespace YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.UserRoleVM
{
    public class UserRoleListVM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int RoleId { get; set; }
        public RoleListVM Role { get; set; }
    }
}
