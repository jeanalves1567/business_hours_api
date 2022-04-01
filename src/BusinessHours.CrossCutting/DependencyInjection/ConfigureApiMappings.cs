using AutoMapper;
using BusinessHours.CrossCutting.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessHours.CrossCutting.DependencyInjection
{
    public static class ConfigureApiMappings
    {
        public static void RegisterMappingProfiles(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BusinessHoursRulesProfile());
                cfg.AddProfile(new DepartmentsProfile());
                cfg.AddProfile(new HolidaysProfile());
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
