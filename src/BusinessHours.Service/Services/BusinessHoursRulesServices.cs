using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessHours.Domain.Dtos.Rules;
using BusinessHours.Domain.Dtos.Validators;
using BusinessHours.Domain.Entities;
using BusinessHours.Domain.Errors;
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

        public async Task<IEnumerable<RuleListDto>> ListBusinessHoursRules()
        {
            var rules = await _rulesRepository.SelectAsync();
            return _mapper.Map<IEnumerable<RuleListDto>>(rules);
        }

        public async Task<RuleReadDto> GetBusinessHoursRule(string ruleId)
        {
            if (string.IsNullOrEmpty(ruleId))
                throw new ArgumentNullException(nameof(ruleId));

            var rule = await _rulesRepository.GetRule(ruleId);
            if (rule == null)
                throw new KeyNotFoundException();

            return _mapper.Map<RuleReadDto>(rule);
        }

        public async Task<RuleReadDto> CreateBusinessHoursRule(RuleCreateDto payload)
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
            return _mapper.Map<RuleReadDto>(result);
        }

        public async Task<RuleReadDto> UpdateBusinessHoursRule(string ruleId, RuleUpdateDto payload)
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
            return _mapper.Map<RuleReadDto>(result);
        }

        public async Task DeleteBusinessHoursRule(string ruleId)
        {
            if (string.IsNullOrEmpty(ruleId))
                throw new ArgumentNullException(nameof(ruleId));

            if (ruleId == "default")
                throw new BadRequestException("The default rule can not be removed");

            var ruleExists = await _rulesRepository.ExistsAsync(ruleId);
            if (!ruleExists)
                throw new KeyNotFoundException();

            await _rulesRepository.DeleteAsync(ruleId);
        }

    }
}
