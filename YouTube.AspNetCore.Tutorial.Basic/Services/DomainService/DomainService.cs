using YouTube.AspNetCore.Tutorial.Basic.Generic_Repositories;
using YouTube.AspNetCore.Tutorial.Basic.MapperApp;
using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.DomainVM;

namespace YouTube.AspNetCore.Tutorial.Basic.Services.DomainService
{
    public class DomainService : GenericService<Domain, DomainListVM, DomainCreateVM, DomainUpdateVM>, IDomainService
    {
        public DomainService(IGenericRepository<Domain> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public List<DomainListVM> GetAllDomainsByRoleId(int roleId)
        {
            var domainList = GetAllItems().Where(x => x.RoleId == roleId).ToList();
            return domainList;
        }

        public List<DomainListVM> GetAllDomainsByControllerNameId(int controllerId)
        {
            var domainList = GetAllItems().Where(x => x.ControllerNameId == controllerId).ToList();
            return domainList;
        }

        public DomainUpdateVM GetitemByModel(ControllerParamsVM model)
        {
            var domainRole = GetItemById(model.Id);
            domainRole.ControllerNameId = model.ControllerId;
            domainRole.ControllerNameForTitle = model.ControllerName;
            return domainRole;
        }

        public List<DomainListVM> GetAllDomainsByControllerName(string controllerName)
        {
            var domainList = GetAllItems().Where(x => x.ControllerName.Name == controllerName).ToList();
            return domainList;
        }
    }
}
