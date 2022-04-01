using System.Threading.Tasks;
using BusinessHours.Domain.Entities;

namespace BusinessHours.Domain.Interfaces.Repositories
{
    public interface IDepartmentsRepository : IRepository<Department>
    {
        Task<Department> GetDepartment(string departmentId);
        Task<Department> GetDepartmentByExternalId(string externalId);
        Task<bool> ExternalIdExists(string externalId);
    }
}
