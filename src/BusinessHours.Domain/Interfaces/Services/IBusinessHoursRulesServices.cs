using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessHours.Domain.Dtos.Rules;

namespace BusinessHours.Domain.Interfaces.Services
{
    public interface IBusinessHoursRulesServices
    {
        Task<IEnumerable<RuleListDto>> ListBusinessHoursRules();
        Task<RuleReadDto> GetBusinessHoursRule(string ruleId);
        Task<RuleReadDto> CreateBusinessHoursRule(RuleCreateDto payload);
        Task<RuleReadDto> UpdateBusinessHoursRule(string ruleId, RuleUpdateDto payload);
        Task DeleteBusinessHoursRule(string ruleId);
    }
}
