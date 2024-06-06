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
        private readonly IGenericRepository<Role> _roleRepository;

        public UserService(IGenericRepository<User> repository, IMapper mapper, IHttpContextAccessor contextAccessor, IGenericRepository<Role> roleRepository) : base(repository, mapper)
        {
            _contextAccessor = contextAccessor;
            _roleRepository = roleRepository;
        }

        public override void CreateItem(UserCreateVM request)
        {
            bool passwordCheck = request.Password == request.PasswordConfirm ? true : false;
            if (!passwordCheck)
            {
                throw new UserServiceExceptions("Password and Password Confirm must be equal");
            }

            var user = _mapper.Map<UserCreateVM, User>(request, 3);
            user.PasswordHash = HashMaker(user, request.Password);

            var memberRole = _roleRepository.GetAll().FirstOrDefault(x => x.RoleName == "Member");
            if (memberRole == null)
            {
                throw new ServerSideExceptions("Role not exist. Please speak your Admin");
            };

            var userRole = new UserRole()
            {
                RoleId = memberRole.Id,
                UserId = user.Id,
            };

            if (user.UserRoles is null)
            {
                user.UserRoles = new List<UserRole>();
            }

            user.UserRoles.Add(userRole);
            _repository.CreateItem(user);

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

            foreach (var userRole in user.UserRoles)
            {
                var roleClaim = new Claim(ClaimTypes.Role, userRole.Role.RoleName);
                claims.Add(roleClaim);
            }

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
            var user = _repository.GetAll().Where(x => x.Id == request.Id).FirstOrDefault();
            if (user == null)
            {
                throw new UserServiceExceptions("Email or Password error");
            }

            var verifyPassword = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (verifyPassword == PasswordVerificationResult.Failed)
            {
                throw new UserServiceExceptions("Email or Password error");
            }

            var mappedUser = _mapper.Map(request, user,3);

            if (request.NewPassword == null)
            {
                _repository.UpdateItem(mappedUser);
                return;
            }

            if (request.NewPassword != request.NewPasswordConfirm)
            {
                throw new UserServiceExceptions("New Password and New Password Confirm must be equal");
            }

            var newPassworsHash = HashMaker(user, request.NewPassword);
            mappedUser.PasswordHash = newPassworsHash;
            _repository.UpdateItem(mappedUser);

        }
    }
}
