using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.RoleVM;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.UserRoleVM;

namespace YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.UserVM
{
    public class UserListVM
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public List<UserRoleListVM> UserRoles { get; set; }
    }
}
