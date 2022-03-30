using Microsoft.Extensions.DependencyInjection;

namespace BusinessHours.Api.Config
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependecies(this IServiceCollection services)
        {
            // Declare the dependencies here
            return services;
        }
    }
}
