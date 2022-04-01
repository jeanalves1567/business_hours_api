using BusinessHours.Domain.Interfaces.Services;
using BusinessHours.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessHours.CrossCutting.DependencyInjection
{
    public static class ConfigureApiServices
    {
        public static void RegisterApiServices(this IServiceCollection services)
        {
            services.AddScoped<IBusinessHoursRulesServices, BusinessHoursRulesServices>();
            services.AddScoped<IDepartmentsServices, DepartmentsServices>();
            services.AddScoped<IHolidaysServices, HolidaysServices>();
        }
    }
}
