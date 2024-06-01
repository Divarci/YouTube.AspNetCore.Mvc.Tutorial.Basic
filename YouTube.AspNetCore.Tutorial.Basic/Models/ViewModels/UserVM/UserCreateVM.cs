using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;

namespace YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.UserVM
{
    public class UserCreateVM
    {
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
