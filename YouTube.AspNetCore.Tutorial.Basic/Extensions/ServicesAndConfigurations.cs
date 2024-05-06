using Microsoft.EntityFrameworkCore;
using YouTube.AspNetCore.Tutorial.Basic.Context;
using YouTube.AspNetCore.Tutorial.Basic.Filters;
using YouTube.AspNetCore.Tutorial.Basic.Generic_Repositories;
using YouTube.AspNetCore.Tutorial.Basic.MapperApp;
using YouTube.AspNetCore.Tutorial.Basic.Services;

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

            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericService<,,,>),typeof(GenericService<,,,>));
            services.AddScoped(typeof(IMapper<,>), typeof(Mapper<,>));

            services.AddScoped(typeof(ParameterCheckFilter<,>));




            return services;
        }
    }
}
