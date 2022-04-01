using System;
using System.Linq;
using AutoMapper;
using BusinessHours.Domain.Dtos.Holidays;
using BusinessHours.Domain.Dtos.Rules;
using BusinessHours.Domain.Entities;

namespace BusinessHours.CrossCutting.Mappings
{
    public class BusinessHoursRulesProfile : Profile
    {
        public BusinessHoursRulesProfile()
        {
            CreateMap<BusinessHoursRule, RuleReadDto>()
                .ForMember(dest => dest.Holidays, opt => opt.MapFrom(src => src.Holidays.Select(rh => new HolidayListDto
                {
                    Id = rh.Holiday.Id,
                    Name = rh.Holiday.Name,
                    Year = rh.Holiday.Year,
                    Month = rh.Holiday.Month,
                    Day = rh.Holiday.Day,
                    AllDay = rh.Holiday.AllDay,
                    Start = rh.Holiday.Start,
                    Finish = rh.Holiday.Finish,
                    CreatedAt = rh.Holiday.CreatedAt.HasValue ? rh.Holiday.CreatedAt.Value : DateTime.UtcNow,
                    UpdatedAt = rh.Holiday.UpdatedAt,
                }).ToList()));
            CreateMap<BusinessHoursRule, RuleListDto>();
            CreateMap<RuleCreateDto, BusinessHoursRule>();
            CreateMap<WorkHours, WorkHoursReadDto>();
            CreateMap<WorkHoursCreateDto, WorkHours>();
        }
    }
}
