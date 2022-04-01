using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessHours.Domain.Dtos;

namespace BusinessHours.Domain.Interfaces.Services
{
    public interface IBusinessHoursRulesServices
    {
        Task<IEnumerable<BusinessHoursRuleListDto>> ListBusinessHoursRules();
        Task<BusinessHoursRuleReadDto> GetBusinessHoursRule(Guid ruleId);
        Task<BusinessHoursRuleReadDto> CreateBusinessHoursRule(BusinessHoursRuleCreateDto payload);
        Task<BusinessHoursRuleReadDto> UpdateBusinessHoursRule(Guid ruleId, BusinessHoursRuleUpdateDto payload);
        Task DeleteBusinessHoursRule(Guid ruleId);
    }
}
