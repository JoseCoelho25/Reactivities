using API.Services;
using Domain;
using Persistence;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentityCore<AppUser>(opt=>
            {
                opt.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<DataContext>();

            services.AddAuthentication();
            //addscoped means this token service will be scoped to the http req
            services.AddScoped<TokenService>();

            return services;
        }
    }
}