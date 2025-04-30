using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.Design;

namespace Ordering.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {



            return services; 
        }

    }
}
