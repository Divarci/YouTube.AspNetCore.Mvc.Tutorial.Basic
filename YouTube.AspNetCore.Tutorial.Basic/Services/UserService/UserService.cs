using YouTube.AspNetCore.Tutorial.Basic.Generic_Repositories;
using YouTube.AspNetCore.Tutorial.Basic.MapperApp;
using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.UserVM;

namespace YouTube.AspNetCore.Tutorial.Basic.Services.UserService
{
    public class UserService : GenericService<User, UserListVM, UserCreateVM, UserUpdateVM> , IUserService
    {
        public UserService(
            IGenericRepository<User> repository, 
            IMapper<User, UserListVM> listMapper, 
            IMapper<UserCreateVM, User> createMapper, 
            IMapper<UserUpdateVM, User> updateMapper, 
            IMapper<User, UserUpdateVM> itemMapper
            ) : base(repository, listMapper, createMapper, updateMapper, itemMapper)
        {
        }

        public override void CreateItem(UserCreateVM request)
        {
            //logic
        }

    }
}
