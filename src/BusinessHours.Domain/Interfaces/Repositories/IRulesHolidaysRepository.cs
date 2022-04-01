using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessHours.Domain.Entities;

namespace BusinessHours.Domain.Interfaces.Repositories
{
    public interface IRulesHolidaysRepository : IDisposable
    {
        Task<IEnumerable<RuleHoliday>> FindByRule(string ruleId);
        Task<IEnumerable<RuleHoliday>> FindByHoliday(string holidayId);
        Task<RuleHoliday> SelectAsync(string ruleId, string holidayId);
        Task<bool> ExistsAsync(string ruleId, string holidayId);
        Task<RuleHoliday> InsertAsync(RuleHoliday data);
        Task DeleteAsync(string ruleId, string holidayId);
        Task<bool> SaveChangesAsync();
    }
}
