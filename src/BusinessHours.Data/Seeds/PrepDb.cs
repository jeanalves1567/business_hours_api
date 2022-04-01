using System;
using System.Collections.Generic;
using System.Linq;
using BusinessHours.Data.Contexts;
using BusinessHours.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessHours.Data.Seeds
{
    public static class PrepDb
    {
        public static void SeedData(AppDbContext context, IConfiguration configuration, bool isProduction = false)
        {
            if (isProduction)
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                try
                {
                    context.Database.Migrate();

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }

            if (!context.Rules.Any())
            {
                var defaultRuleId = "default";
                var defaultRule = new BusinessHoursRule
                {
                    Id = defaultRuleId,
                    Name = "Default",
                    Timezone = TimeZoneInfo.Local.Id,
                    CreatedAt = DateTime.UtcNow,
                    WorkHours = new List<WorkHours>
                    {
                        new WorkHours
                        {
                            RuleId = defaultRuleId,
                            Day = DayOfWeek.Sunday,
                            Open = true,
                            Start = "00:00",
                            Finish = "23:59"
                        },
                        new WorkHours
                        {
                            RuleId = defaultRuleId,
                            Day = DayOfWeek.Monday,
                            Open = true,
                            Start = "00:00",
                            Finish = "23:59"
                        },
                        new WorkHours
                        {
                            RuleId = defaultRuleId,
                            Day = DayOfWeek.Tuesday,
                            Open = true,
                            Start = "00:00",
                            Finish = "23:59"
                        },
                        new WorkHours
                        {
                            RuleId = defaultRuleId,
                            Day = DayOfWeek.Wednesday,
                            Open = true,
                            Start = "00:00",
                            Finish = "23:59"
                        },
                        new WorkHours
                        {
                            RuleId = defaultRuleId,
                            Day = DayOfWeek.Thursday,
                            Open = true,
                            Start = "00:00",
                            Finish = "23:59"
                        },
                        new WorkHours
                        {
                            RuleId = defaultRuleId,
                            Day = DayOfWeek.Friday,
                            Open = true,
                            Start = "00:00",
                            Finish = "23:59"
                        },
                        new WorkHours
                        {
                            RuleId = defaultRuleId,
                            Day = DayOfWeek.Saturday,
                            Open = true,
                            Start = "00:00",
                            Finish = "23:59"
                        },
                    }
                };
                Console.WriteLine("--> Seeding data...");
                context.Rules.AddRange(defaultRule);
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}
