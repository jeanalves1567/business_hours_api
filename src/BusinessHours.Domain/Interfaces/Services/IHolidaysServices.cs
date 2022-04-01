using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessHours.Domain.Dtos.Holidays;

namespace BusinessHours.Domain.Interfaces.Services
{
    public interface IHolidaysServices
    {
        Task<IEnumerable<HolidayListDto>> ListHolidays();
        Task<HolidayReadDto> GetHoliday(string holidayId);
        Task<HolidayReadDto> CreateHoliday(HolidayCreateDto payload);
        Task<HolidayReadDto> UpdateHoliday(string holidayId, HolidayUpdateDto payload);
        Task DeleteHoliday(string holidayId);
        Task AssignToRule(string holidayId, string ruleId);
        Task UnassignRule(string holidayId, string ruleId);
    }
}
