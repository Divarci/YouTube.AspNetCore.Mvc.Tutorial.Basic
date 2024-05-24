using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using YouTube.AspNetCore.Tutorial.Basic.Exceptions;
using YouTube.AspNetCore.Tutorial.Basic.Generic_Repositories;
using YouTube.AspNetCore.Tutorial.Basic.MapperApp;
using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.UserVM;

namespace YouTube.AspNetCore.Tutorial.Basic.Services.UserService
{
    public class UserService : GenericService<User, UserListVM, UserCreateVM, UserUpdateVM>, IUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(
            IGenericRepository<User> repository,
            IMapper<User, UserListVM> listMapper,
            IMapper<UserCreateVM, User> createMapper,
            IMapper<UserUpdateVM, User> updateMapper,
            IMapper<User, UserUpdateVM> itemMapper,
            IHttpContextAccessor contextAccessor) : base(repository, listMapper, createMapper, updateMapper, itemMapper)
        {
            _contextAccessor = contextAccessor;
        }

        public override void CreateItem(UserCreateVM request)
        {
            bool passwordCheck = request.Password == request.PasswordConfirm ? true : false;
            if (!passwordCheck)
            {
                throw new UserServiceExceptions("Password and Password Confirm must be equal");
            }

            var user = _CreateMapper.Map<UserCreateVM, User>(request);
            user.PasswordHash = HashMaker(user, request.Password); _repository.CreateItem(user);

        }

        private string HashMaker(User user, string password)
        {
            var hashedPassword = new PasswordHasher<User>().HashPassword(user, password);
            return hashedPassword;
        }

        public bool SignIn(string email, string propvidedPassword)
        {
            var user = _repository.GetAll().Where(x => x.Email == email).FirstOrDefault();
            if (user == null)
            {
                //log
                return false;
            }

            var passwordVerify = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, propvidedPassword);
            if (passwordVerify == PasswordVerificationResult.Failed)
            {
                return false;
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Email,user.Email),
                new(ClaimTypes.Name,user.Fullname),
                new(ClaimTypes.NameIdentifier,user.Id.ToString())
            };

            //add roles

            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));
            return true;
        }

        public void SignOut()
        {
            _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
        }

        public override void UpdateItem(UserUpdateVM request)
        {
            var user = _repository.GetAll().Where(x => x.Email == request.Email).FirstOrDefault();
            if (user == null)
            {
                throw new UserServiceExceptions("Email or Password error");
            }

            var verifyPassword = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (verifyPassword == PasswordVerificationResult.Failed)
            {
                throw new UserServiceExceptions("Email or Password error");
            }

            var mappedUser = _UpdateMapper.Map(request, user);
            
            if(request.NewPassword == null)
            {
                _repository.UpdateItem(mappedUser);
                return;
            }

            if (request.NewPassword != request.NewPasswordConfirm)
            {
                throw new UserServiceExceptions("New Password and New Password Confirm must be equal");
            }

            var newPassworsHash = HashMaker(user,request.NewPassword);
            mappedUser.PasswordHash = newPassworsHash;
            _repository.UpdateItem(mappedUser);

        }
    }
}
