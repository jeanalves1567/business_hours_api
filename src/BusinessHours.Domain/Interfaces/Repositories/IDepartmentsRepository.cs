using System.Threading.Tasks;
using BusinessHours.Domain.Entities;

namespace BusinessHours.Domain.Interfaces.Repositories
{
    public interface IDepartmentsRepository : IRepository<Department>
    {
        Task<Department> GetDepartment(string departmentId);
    }
}
