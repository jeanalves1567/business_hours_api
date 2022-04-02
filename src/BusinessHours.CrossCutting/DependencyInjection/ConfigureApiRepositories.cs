using System;
using BusinessHours.Data.Contexts;
using BusinessHours.Data.Repositories;
using BusinessHours.Domain.Errors;
using BusinessHours.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace BusinessHours.CrossCutting.DependencyInjection
{
    public static class ConfigureApiRepositories
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRulesRepository, RulesRepository>();
            services.AddScoped<IDepartmentsRepository, DepartmentsRepository>();
            services.AddScoped<IHolidaysRepository, HolidaysRepository>();
            services.AddScoped<IRulesHolidaysRepository, RulesHolidaysRepository>();
            services.AddDbContext<AppDbContext>(options =>
            {
                string DB_CONNECTION_STRING = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
                if (string.IsNullOrEmpty(DB_CONNECTION_STRING)) throw new MissingEnvironmentVariableException("DB_CONNECTION_STRING");

                string DB_TYPE = Environment.GetEnvironmentVariable("DB_TYPE").ToLower();
                if (string.IsNullOrEmpty(DB_TYPE)) throw new MissingEnvironmentVariableException("DB_TYPE");

                if (DB_TYPE == "mysql") options.UseMySql(DB_CONNECTION_STRING, ServerVersion.AutoDetect(DB_CONNECTION_STRING));
                else options.UseSqlServer(DB_CONNECTION_STRING);
            });
        }
    }
}
