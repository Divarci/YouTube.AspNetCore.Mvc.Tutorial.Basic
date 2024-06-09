using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.ControllerVM;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.RoleVM;

namespace YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.DomainVM
{
    public class DomainListVM
    {
        public int Id { get; set; }

        public int ControllerNameId { get; set; }
        public ControllerListVM ControllerName { get; set; }

        public int RoleId { get; set; }
        public RoleListVM Role { get; set; }
    }
}
