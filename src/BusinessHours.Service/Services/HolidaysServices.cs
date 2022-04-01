using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessHours.Domain.Dtos.Holidays;
using BusinessHours.Domain.Dtos.Validators;
using BusinessHours.Domain.Entities;
using BusinessHours.Domain.Interfaces.Repositories;
using BusinessHours.Domain.Interfaces.Services;

namespace BusinessHours.Service.Services
{
    public class HolidaysServices : IHolidaysServices
    {
        private readonly IHolidaysRepository _holidaysRepository;
        private readonly IRulesHolidaysRepository _rulesHolidaysRepository;
        private readonly IRulesRepository _rulesRepository;
        private readonly IMapper _mapper;

        public HolidaysServices
        (
            IHolidaysRepository holidaysRepository,
            IRulesHolidaysRepository rulesHolidaysRepository,
            IRulesRepository rulesRepository,
            IMapper mapper
        )
        {
            _holidaysRepository = holidaysRepository;
            _rulesHolidaysRepository = rulesHolidaysRepository;
            _rulesRepository = rulesRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HolidayListDto>> ListHolidays()
        {
            var holidays = await _holidaysRepository.SelectAsync();
            return _mapper.Map<IEnumerable<HolidayListDto>>(holidays);
        }

        public async Task<HolidayReadDto> GetHoliday(string holidayId)
        {
            if (string.IsNullOrEmpty(holidayId)) throw new ArgumentNullException("holidayId");
            var holiday = await _holidaysRepository.GetHoliday(holidayId);
            if (holiday == null) throw new KeyNotFoundException();
            return _mapper.Map<HolidayReadDto>(holiday);
        }

        public async Task<HolidayReadDto> CreateHoliday(HolidayCreateDto payload)
        {
            payload.Validate();
            var holiday = _mapper.Map<Holiday>(payload);
            var result = await _holidaysRepository.InsertAsync(holiday);
            return _mapper.Map<HolidayReadDto>(result);
        }

        public async Task<HolidayReadDto> UpdateHoliday(string holidayId, HolidayUpdateDto payload)
        {
            payload.Validate();
            if (string.IsNullOrEmpty(holidayId)) throw new ArgumentNullException("holidayId");
            var holiday = await _holidaysRepository.GetHoliday(holidayId);
            if (holiday == null) throw new KeyNotFoundException();
            if (!string.IsNullOrEmpty(payload.Name) && payload.Name != holiday.Name) holiday.Name = payload.Name;
            if (payload.Year.HasValue) holiday.Year = (payload.Year.Value == 0) ? null : payload.Year.Value;
            if (payload.Month.HasValue) holiday.Month = payload.Month.Value;
            if (payload.Day.HasValue) holiday.Day = payload.Day.Value;
            var checkDate = new DateTime(holiday.Year.HasValue ? holiday.Year.Value : DateTime.Now.Year, holiday.Month, holiday.Day);
            if (payload.AllDay.HasValue && payload.AllDay.Value != holiday.AllDay) holiday.AllDay = payload.AllDay.Value;
            if (holiday.AllDay)
            {
                holiday.Start = null;
                holiday.Finish = null;
            }
            else
            {
                if (!string.IsNullOrEmpty(payload.Start) && payload.Start != holiday.Start) holiday.Start = payload.Start;
                if (!string.IsNullOrEmpty(payload.Finish) && payload.Finish != holiday.Finish) holiday.Finish = payload.Finish;
            }
            var result = await _holidaysRepository.UpdateAsync(holiday);
            return _mapper.Map<HolidayReadDto>(result);
        }

        public async Task DeleteHoliday(string holidayId)
        {
            if (string.IsNullOrEmpty(holidayId)) throw new ArgumentNullException("holidayId");
            var holidayExists = await _holidaysRepository.ExistsAsync(holidayId);
            if (!holidayExists) throw new KeyNotFoundException();
            await _holidaysRepository.DeleteAsync(holidayId);
        }

        public async Task AssignToRule(string holidayId, string ruleId)
        {
            if (string.IsNullOrEmpty(holidayId)) throw new ArgumentNullException("holidayId");
            if (string.IsNullOrEmpty(ruleId)) throw new ArgumentNullException("ruleId");
            var holiday = await _holidaysRepository.SelectAsync(holidayId);
            if (holiday == null) throw new KeyNotFoundException();
            var rule = await _rulesRepository.SelectAsync(ruleId);
            if (rule == null) throw new KeyNotFoundException();
            var associationExists = await _rulesHolidaysRepository.ExistsAsync(ruleId, holidayId);
            if (associationExists) return;
            var ruleHoliday = new RuleHoliday
            {
                Holiday = holiday,
                Rule = rule,
            };
            var result = await _rulesHolidaysRepository.InsertAsync(ruleHoliday);
        }

        public async Task UnassignRule(string holidayId, string ruleId)
        {
            if (string.IsNullOrEmpty(holidayId)) throw new ArgumentNullException("holidayId");
            if (string.IsNullOrEmpty(ruleId)) throw new ArgumentNullException("ruleId");
            var holiday = await _holidaysRepository.SelectAsync(holidayId);
            if (holiday == null) throw new KeyNotFoundException();
            var rule = await _rulesRepository.SelectAsync(ruleId);
            if (rule == null) throw new KeyNotFoundException();
            var associationExists = await _rulesHolidaysRepository.ExistsAsync(ruleId, holidayId);
            if (!associationExists) return;
            await _rulesHolidaysRepository.DeleteAsync(ruleId, holidayId);
        }
    }
}
