using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessHours.Data.Contexts;
using BusinessHours.Domain.Entities;
using BusinessHours.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BusinessHours.Data.Repositories
{
    public class RulesHolidaysRepository : IRulesHolidaysRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<RuleHoliday> _dataset;

        public RulesHolidaysRepository(AppDbContext context)
        {
            _context = context;
            _dataset = context.Set<RuleHoliday>();
        }

        public async Task<IEnumerable<RuleHoliday>> FindByHoliday(string holidayId)
        {
            return await _dataset.AsNoTracking().Include(rh => rh.Rule).Where(rh => rh.HolidayId == holidayId).ToListAsync();
        }

        public async Task<IEnumerable<RuleHoliday>> FindByRule(string ruleId)
        {
            return await _dataset.AsNoTracking().Include(rh => rh.Holiday).Where(rh => rh.RuleId == ruleId).ToListAsync();
        }

        public async Task<RuleHoliday> SelectAsync(string ruleId, string holidayId)
        {
            return await _dataset.AsNoTracking().Include(rh => rh.Holiday).Include(rh => rh.Rule).FirstOrDefaultAsync(rh => rh.RuleId == ruleId && rh.HolidayId == holidayId);
        }

        public async Task<bool> ExistsAsync(string ruleId, string holidayId)
        {
            return await _dataset.AnyAsync(rh => rh.RuleId == ruleId && rh.HolidayId == holidayId);
        }

        public async Task<RuleHoliday> InsertAsync(RuleHoliday data)
        {
            _dataset.Add(data);
            await SaveChangesAsync();
            return data;
        }

        public async Task DeleteAsync(string ruleId, string holidayId)
        {
            _dataset.Remove(new RuleHoliday { RuleId = ruleId, HolidayId = holidayId });
            await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            var savedItems = await _context.SaveChangesAsync();
            return savedItems > 0;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

    }
}
