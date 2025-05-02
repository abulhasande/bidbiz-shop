using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.Design;
using System.Reflection;

namespace Ordering.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices            
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(mR =>
            {
                mR.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            return services; 
        }

    }
}
