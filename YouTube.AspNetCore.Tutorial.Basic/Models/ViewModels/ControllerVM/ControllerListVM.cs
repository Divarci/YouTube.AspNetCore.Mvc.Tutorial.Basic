using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.DomainVM;

namespace YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.ControllerVM
{
    public class ControllerListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<DomainListVM> DomainList { get; set; }
    }
}
