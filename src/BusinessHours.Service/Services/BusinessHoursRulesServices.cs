using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessHours.Domain.Dtos;
using BusinessHours.Domain.Dtos.Validators;
using BusinessHours.Domain.Entities;
using BusinessHours.Domain.Interfaces.Repositories;
using BusinessHours.Domain.Interfaces.Services;

namespace BusinessHours.Service.Services
{
    public class BusinessHoursRulesServices : IBusinessHoursRulesServices
    {
        private readonly IRulesRepository _rulesRepository;
        private readonly IMapper _mapper;

        public BusinessHoursRulesServices(IRulesRepository rulesRepository, IMapper mapper)
        {
            _rulesRepository = rulesRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BusinessHoursRuleListDto>> ListBusinessHoursRules()
        {
            var rules = await _rulesRepository.SelectAsync();
            return _mapper.Map<IEnumerable<BusinessHoursRuleListDto>>(rules);
        }

        public async Task<BusinessHoursRuleReadDto> GetBusinessHoursRule(Guid ruleId)
        {
            if (ruleId == Guid.Empty)
                throw new ArgumentNullException(nameof(ruleId));

            var rule = await _rulesRepository.GetRule(ruleId);
            if (rule == null)
                throw new KeyNotFoundException();

            return _mapper.Map<BusinessHoursRuleReadDto>(rule);
        }

        public async Task<BusinessHoursRuleReadDto> CreateBusinessHoursRule(BusinessHoursRuleCreateDto payload)
        {
            payload.Validate();

            var workHours = new List<WorkHours>();
            foreach (var day in Enum.GetValues<DayOfWeek>())
            {
                var hours = new WorkHours();
                hours.Day = day;
                var workHour = payload.WorkHours.FirstOrDefault(d => d.Day.ToUpper().Equals(day.ToString().ToUpper()));
                if (workHour == null)
                {
                    hours.Open = false;
                }
                else
                {
                    hours.Open = workHour.Open;
                    if (hours.Open)
                    {
                        hours.Start = workHour.Start;
                        hours.Finish = workHour.Finish;
                    }
                }
                workHours.Add(hours);
            }
            var rule = _mapper.Map<BusinessHoursRule>(payload);
            rule.WorkHours = workHours;

            var result = await _rulesRepository.InsertAsync(rule);
            return _mapper.Map<BusinessHoursRuleReadDto>(result);
        }

        public async Task<BusinessHoursRuleReadDto> UpdateBusinessHoursRule(Guid ruleId, BusinessHoursRuleUpdateDto payload)
        {
            payload.Validate();
            var rule = await _rulesRepository.GetRule(ruleId);
            if (rule == null) throw new KeyNotFoundException();
            if (!string.IsNullOrEmpty(payload.Name) && payload.Name != rule.Name) rule.Name = payload.Name;
            if (!string.IsNullOrEmpty(payload.Timezone) && payload.Timezone != rule.Timezone) rule.Timezone = payload.Timezone;
            if (payload.WorkHours != null)
            {
                var workHours = new List<WorkHours>();
                foreach (var day in Enum.GetValues<DayOfWeek>())
                {
                    var hours = new WorkHours();
                    hours.Day = day;
                    hours.RuleId = rule.Id;
                    var workHour = payload.WorkHours.FirstOrDefault(d => d.Day.ToUpper().Equals(day.ToString().ToUpper()));
                    if (workHour == null)
                    {
                        hours.Open = false;
                    }
                    else
                    {
                        hours.Open = workHour.Open;
                        if (hours.Open)
                        {
                            hours.Start = workHour.Start;
                            hours.Finish = workHour.Finish;
                        }
                    }
                    workHours.Add(hours);
                }
                rule.WorkHours = workHours;
            }

            var result = await _rulesRepository.UpdateAsync(rule);
            return _mapper.Map<BusinessHoursRuleReadDto>(result);
        }

        public async Task DeleteBusinessHoursRule(Guid ruleId)
        {
            if (ruleId == Guid.Empty)
                throw new ArgumentNullException(nameof(ruleId));

            var ruleExists = await _rulesRepository.ExistsAsync(ruleId);
            if (!ruleExists)
                throw new KeyNotFoundException();

            await _rulesRepository.DeleteAsync(ruleId);
        }

    }
}
