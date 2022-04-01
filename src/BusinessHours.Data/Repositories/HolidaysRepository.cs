using System.Threading.Tasks;
using BusinessHours.Data.Contexts;
using BusinessHours.Domain.Entities;
using BusinessHours.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BusinessHours.Data.Repositories
{
    public class HolidaysRepository : Repository<Holiday>, IHolidaysRepository
    {
        public HolidaysRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Holiday> GetHoliday(string holidayId)
        {
            return await Context.Holidays.AsNoTracking().Include(h => h.Rules).ThenInclude(rh => rh.Rule).FirstOrDefaultAsync(h => h.Id == holidayId);
        }
    }
}
