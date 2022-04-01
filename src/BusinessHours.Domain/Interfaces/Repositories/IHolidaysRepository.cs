using System.Threading.Tasks;
using BusinessHours.Domain.Entities;

namespace BusinessHours.Domain.Interfaces.Repositories
{
    public interface IHolidaysRepository : IRepository<Holiday>
    {
        Task<Holiday> GetHoliday(string holidayId);
    }
}
