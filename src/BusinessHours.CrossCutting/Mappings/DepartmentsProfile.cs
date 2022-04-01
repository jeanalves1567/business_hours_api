using AutoMapper;
using BusinessHours.Domain.Dtos.Departments;
using BusinessHours.Domain.Entities;

namespace BusinessHours.CrossCutting.Mappings
{
    public class DepartmentsProfile : Profile
    {
        public DepartmentsProfile()
        {
            CreateMap<Department, DepartmentReadDto>();
            CreateMap<Department, DepartmentListDto>();
            CreateMap<DepartmentCreateDto, Department>();
        }
    }
}
