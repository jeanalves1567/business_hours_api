using AutoMapper;
using BusinessHours.Domain.Dtos.Rules;
using BusinessHours.Domain.Entities;

namespace BusinessHours.CrossCutting.Mappings
{
    public class BusinessHoursRulesProfile : Profile
    {
        public BusinessHoursRulesProfile()
        {
            CreateMap<BusinessHoursRule, RuleReadDto>();
            CreateMap<BusinessHoursRule, RuleListDto>();
            CreateMap<RuleCreateDto, BusinessHoursRule>();
            CreateMap<WorkHours, WorkHoursReadDto>();
            CreateMap<WorkHoursCreateDto, WorkHours>();
        }
    }
}
