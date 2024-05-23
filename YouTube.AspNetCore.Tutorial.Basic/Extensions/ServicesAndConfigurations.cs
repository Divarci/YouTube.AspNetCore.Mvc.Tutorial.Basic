using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using YouTube.AspNetCore.Tutorial.Basic.Context;
using YouTube.AspNetCore.Tutorial.Basic.Filters;
using YouTube.AspNetCore.Tutorial.Basic.Generic_Repositories;
using YouTube.AspNetCore.Tutorial.Basic.MapperApp;
using YouTube.AspNetCore.Tutorial.Basic.Services;
using YouTube.AspNetCore.Tutorial.Basic.Services.UserService;

namespace YouTube.AspNetCore.Tutorial.Basic.Extensions
{
    public static class ServicesAndConfigurations
    {
        public static IServiceCollection LoadServicesAndConfigs(this IServiceCollection services, IConfiguration config)
        {

            services.AddDbContext<CustomContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("SqlConnection"));
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericService<,,,>), typeof(GenericService<,,,>));
            services.AddScoped(typeof(IMapper<,>), typeof(Mapper<,>));

            services.AddScoped(typeof(ParameterCheckFilter<,>));

            services.AddScoped<IUserService, UserService>();

            services.AddHttpContextAccessor();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                var newCookie = new CookieBuilder();
                newCookie.Name = "ByteVerse";
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.LoginPath = "/Authenticate/Login";
                options.LogoutPath = "/Authenticated/Logout";
                //options.AccessDeniedPath = "";
                options.Cookie = newCookie;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            });



            return services;
        }
    }
}
