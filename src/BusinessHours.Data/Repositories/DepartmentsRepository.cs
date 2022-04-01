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

        public async Task<bool> ExternalIdExists(string externalId)
        {
            return await Context.Departments.AsNoTracking().AnyAsync(d => d.ExternalId == externalId);
        }

        public async Task<Department> GetDepartment(string departmentId)
        {
            return await Context.Departments.AsNoTracking()
                .Include(d => d.Rule)
                .Include(d => d.Rule.WorkHours)
                .Include(d => d.Rule.Holidays)
                .ThenInclude(rh => rh.Holiday)
                .FirstOrDefaultAsync(d => d.Id == departmentId);
        }

        public async Task<Department> GetDepartmentByExternalId(string externalId)
        {
            return await Context.Departments.AsNoTracking()
                .Include(d => d.Rule)
                .Include(d => d.Rule.WorkHours)
                .Include(d => d.Rule.Holidays)
                .ThenInclude(rh => rh.Holiday)
                .FirstOrDefaultAsync(d => d.ExternalId == externalId);
        }
    }
}
