using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.DomainVM;

namespace YouTube.AspNetCore.Tutorial.Basic.Services.DomainService
{
    public interface IDomainService : IGenericService<Domain, DomainListVM, DomainCreateVM, DomainUpdateVM>
    {
        List<DomainListVM> GetAllDomainsByRoleId(int roleId);
        List<DomainListVM> GetAllDomainsByControllerNameId(int controllerId);
        DomainUpdateVM GetitemByModel(ControllerParamsVM model);
        List<DomainListVM> GetAllDomainsByControllerName(string controllerName);
    }
}
