using System.Linq;
using AutoMapper;
using BusinessHours.Domain.Dtos.Holidays;
using BusinessHours.Domain.Dtos.Rules;
using BusinessHours.Domain.Entities;

namespace BusinessHours.CrossCutting.Mappings
{
    public class HolidaysProfile : Profile
    {
        public HolidaysProfile()
        {
            CreateMap<Holiday, HolidayListDto>();
            CreateMap<Holiday, HolidayReadDto>()
                .ForMember(dest => dest.Rules, opt => opt.MapFrom(src => src.Rules.Select(rh => new RuleListDto
                {
                    Id = rh.Rule.Id,
                    Name = rh.Rule.Name,
                    Timezone = rh.Rule.Timezone,
                }).ToList()));
            CreateMap<HolidayCreateDto, Holiday>();
        }
    }
}
