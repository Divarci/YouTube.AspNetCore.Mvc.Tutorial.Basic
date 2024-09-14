using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using YouTube.AspNetCore.Tutorial.Basic.Generic_Repositories;
using YouTube.AspNetCore.Tutorial.Basic.MapperApp;
using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.ControllerVM;

namespace YouTube.AspNetCore.Tutorial.Basic.Services.ControllerNameService
{
    public class ControllerNameService : GenericService<ControllerName, ControllerListVM, ControllerCreateVM, ControllerUpdateVM>, IControllerNameService
    {
        public ControllerNameService(IGenericRepository<ControllerName> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public void RefreshControllers()
        {
            var controllerListOnProject = Assembly.GetExecutingAssembly()
                .GetTypes().Where(type => typeof(Controller)
                .IsAssignableFrom(type) || typeof(ControllerBase)
                .IsAssignableFrom(type))
                .ToDictionary(x => x.Name.Substring(0, x.Name.LastIndexOf("Controller")));

            var controllerListOnDb = _repository.GetAll().ToList();

            foreach (var controller in controllerListOnDb)
            {
                if (!controllerListOnProject.TryGetValue(controller.Name, out var type))
                {
                    _repository.DeleteItem(controller);
                }
            }

            var controllerNewListOnDb = _repository.GetAll().ToList();

            foreach (var controllerName in controllerListOnProject)
            {
                if (controllerNewListOnDb.Any(x => x.Name == controllerName.Key))
                {
                    continue;
                }

                _repository.CreateItem(new ControllerName
                {
                    Name = controllerName.Key
                });
            }
        }
    }
}
