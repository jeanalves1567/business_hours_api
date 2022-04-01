using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessHours.Domain.Dtos;

namespace BusinessHours.Domain.Interfaces.Services
{
    public interface IBusinessHoursRulesServices
    {
        Task<IEnumerable<BusinessHoursRuleListDto>> ListBusinessHoursRules();
        Task<BusinessHoursRuleReadDto> GetBusinessHoursRule(string ruleId);
        Task<BusinessHoursRuleReadDto> CreateBusinessHoursRule(BusinessHoursRuleCreateDto payload);
        Task<BusinessHoursRuleReadDto> UpdateBusinessHoursRule(string ruleId, BusinessHoursRuleUpdateDto payload);
        Task DeleteBusinessHoursRule(string ruleId);
    }
}
