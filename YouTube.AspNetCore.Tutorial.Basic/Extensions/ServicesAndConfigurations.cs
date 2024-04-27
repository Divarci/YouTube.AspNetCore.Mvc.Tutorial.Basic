using Microsoft.EntityFrameworkCore;
using YouTube.AspNetCore.Tutorial.Basic.Context;

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

            return services;
        }
    }
}
