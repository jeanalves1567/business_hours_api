using System.Threading.Tasks;
using BusinessHours.Data.Contexts;
using BusinessHours.Domain.Entities;
using BusinessHours.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BusinessHours.Data.Repositories
{
    public class DepartmentsRepository : Repository<Department>, IDepartmentsRepository
    {
        public DepartmentsRepository(AppDbContext context) : base(context) { }

        public async Task<Department> GetDepartment(string departmentId)
        {
            return await Context.Departments.AsNoTracking().Include(d => d.Rule).Include(d => d.Rule.WorkHours).FirstOrDefaultAsync(d => d.Id == departmentId);
        }
    }
}
