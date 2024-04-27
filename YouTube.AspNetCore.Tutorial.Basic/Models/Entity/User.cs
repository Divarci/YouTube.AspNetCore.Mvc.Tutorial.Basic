namespace YouTube.AspNetCore.Tutorial.Basic.Models.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
