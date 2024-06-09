using Microsoft.AspNetCore.Mvc;

namespace YouTube.AspNetCore.Tutorial.Basic.Models.Entity
{
    public class Domain
    {
        public int Id { get; set; }

        public int ControllerNameId { get; set; }
        public ControllerName ControllerName { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
