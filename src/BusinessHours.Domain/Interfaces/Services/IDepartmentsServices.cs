using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessHours.Domain.Dtos.Departments;

namespace BusinessHours.Domain.Interfaces.Services
{
    public interface IDepartmentsServices
    {
        Task<IEnumerable<DepartmentListDto>> ListDepartments();
        Task<DepartmentReadDto> GetDepartment(string departmentId);
        Task<DepartmentReadDto> CreateDepartment(DepartmentCreateDto payload);
        Task<DepartmentReadDto> UpdateDepartment(string departmentId, DepartmentUpdateDto payload);
        Task DeleteDepartment(string departmentId);
        Task<DepartmentMomentStatus> CheckDepartmentWorkingHours(string departmentId);
    }
}
