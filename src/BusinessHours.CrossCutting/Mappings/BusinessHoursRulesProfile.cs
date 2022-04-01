using AutoMapper;
using BusinessHours.Domain.Dtos;
using BusinessHours.Domain.Entities;

namespace BusinessHours.CrossCutting.Mappings
{
    public class BusinessHoursRulesProfile : Profile
    {
        public BusinessHoursRulesProfile()
        {
            CreateMap<BusinessHoursRule, BusinessHoursRuleReadDto>();
            CreateMap<BusinessHoursRule, BusinessHoursRuleListDto>();
            CreateMap<BusinessHoursRuleCreateDto, BusinessHoursRule>();
            CreateMap<WorkHours, WorkHoursReadDto>();
            CreateMap<WorkHoursCreateDto, WorkHours>();
        }
    }
}
