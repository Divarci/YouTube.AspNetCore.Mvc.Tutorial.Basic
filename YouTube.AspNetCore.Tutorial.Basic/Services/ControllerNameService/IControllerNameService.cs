using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.ControllerVM;

namespace YouTube.AspNetCore.Tutorial.Basic.Services.ControllerNameService
{
    public interface IControllerNameService : IGenericService<ControllerName, ControllerListVM, ControllerCreateVM, ControllerUpdateVM>
    {
        void RefreshControllers();
    }


}
