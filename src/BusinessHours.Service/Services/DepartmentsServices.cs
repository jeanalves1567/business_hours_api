using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessHours.Domain.Dtos.Departments;
using BusinessHours.Domain.Dtos.Rules;
using BusinessHours.Domain.Dtos.Validators;
using BusinessHours.Domain.Entities;
using BusinessHours.Domain.Errors;
using BusinessHours.Domain.Interfaces.Repositories;
using BusinessHours.Domain.Interfaces.Services;

namespace BusinessHours.Service.Services
{
    public class DepartmentsServices : IDepartmentsServices
    {
        private readonly IDepartmentsRepository _departmentsRepository;
        private readonly IRulesRepository _rulesRepository;
        private readonly IMapper _mapper;

        public DepartmentsServices(IDepartmentsRepository departmentsRepository, IRulesRepository rulesRepository, IMapper mapper)
        {
            _departmentsRepository = departmentsRepository;
            _rulesRepository = rulesRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentListDto>> ListDepartments()
        {
            var departments = await _departmentsRepository.SelectAsync();
            return _mapper.Map<IEnumerable<DepartmentListDto>>(departments);
        }

        public async Task<DepartmentReadDto> GetDepartment(string departmentId)
        {
            if (string.IsNullOrEmpty(departmentId)) throw new ArgumentNullException("departmentId");
            var department = await _departmentsRepository.GetDepartment(departmentId);
            if (department == null) throw new KeyNotFoundException();
            return _mapper.Map<DepartmentReadDto>(department);
        }

        public async Task<DepartmentReadDto> CreateDepartment(DepartmentCreateDto payload)
        {
            payload.Validate();
            var externalIdExists = await _departmentsRepository.ExternalIdExists(payload.ExternalId);
            if (externalIdExists) throw new BadRequestException("External ID already exists");
            if (!string.IsNullOrEmpty(payload.RuleId))
            {
                var ruleExists = await _rulesRepository.ExistsAsync(payload.RuleId);
                if (!ruleExists) throw new InvalidBodyParamException("ruleId");
            }
            else payload.RuleId = "default";
            var department = _mapper.Map<Department>(payload);
            var result = await _departmentsRepository.InsertAsync(department);
            return _mapper.Map<DepartmentReadDto>(result);
        }

        public async Task<DepartmentReadDto> UpdateDepartment(string departmentId, DepartmentUpdateDto payload)
        {
            if (string.IsNullOrEmpty(departmentId)) throw new ArgumentNullException("departmentId");
            var department = await _departmentsRepository.GetDepartment(departmentId);
            if (department == null) throw new KeyNotFoundException();
            if (!string.IsNullOrEmpty(payload.Name) && payload.Name != department.Name) department.Name = payload.Name;
            if (!string.IsNullOrEmpty(payload.Type) && payload.Type != department.Type) department.Type = payload.Type;
            if (!string.IsNullOrEmpty(payload.ExternalId) && payload.ExternalId != department.ExternalId)
            {
                var externalIdExists = await _departmentsRepository.ExternalIdExists(payload.ExternalId);
                if (externalIdExists) throw new BadRequestException("External ID already exists");
                department.ExternalId = payload.ExternalId;
            }
            if (!string.IsNullOrEmpty(payload.RuleId) && payload.RuleId != department.RuleId)
            {
                var rule = await _rulesRepository.SelectAsync(payload.RuleId);
                if (rule == null) throw new InvalidBodyParamException("ruleId");
                department.Rule = rule;
            }
            var result = await _departmentsRepository.UpdateAsync(department);
            return _mapper.Map<DepartmentReadDto>(result);
        }

        public async Task DeleteDepartment(string departmentId)
        {
            if (string.IsNullOrEmpty(departmentId)) throw new ArgumentNullException("departmentId");
            var departmentExists = await _departmentsRepository.ExistsAsync(departmentId);
            if (!departmentExists) throw new KeyNotFoundException();
            await _departmentsRepository.DeleteAsync(departmentId);
        }

        public async Task<DepartmentMomentStatus> CheckDepartmentWorkingHours(string departmentId)
        {
            if (string.IsNullOrEmpty(departmentId)) throw new ArgumentNullException("departmentId");
            var department = await _departmentsRepository.GetDepartment(departmentId);
            if (department == null) throw new KeyNotFoundException();
            var result = new DepartmentMomentStatus();
            result.Rule = _mapper.Map<RuleReadDto>(department.Rule);
            result.AtWorkingHours = false;

            var utcNow = DateTime.UtcNow;
            var timezone = TimeZoneInfo.FindSystemTimeZoneById(department.Rule.Timezone);
            var now = TimeZoneInfo.ConvertTimeFromUtc(utcNow, timezone);

            // TODO
            // Check if holiday

            // Check if the date is open
            var currentWeekDay = department.Rule.WorkHours.FirstOrDefault(d => d.Day.Equals(now.DayOfWeek));
            if (currentWeekDay == null || !currentWeekDay.Open) return result;

            // Check if the time is between the range defined for the day
            var start = DateTime.Parse(now.ToString("yyyy-MM-dd ") + currentWeekDay.Start + ":00");
            var end = DateTime.Parse(now.ToString("yyyy-MM-dd ") + currentWeekDay.Finish + ":00").AddMinutes(1);
            if (now >= start && now < end) result.AtWorkingHours = true;

            return result;
        }
    }
}
