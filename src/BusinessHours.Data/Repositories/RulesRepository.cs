using System;
using System.Linq;
using System.Threading.Tasks;
using BusinessHours.Data.Contexts;
using BusinessHours.Domain.Entities;
using BusinessHours.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BusinessHours.Data.Repositories
{
    public class RulesRepository : Repository<BusinessHoursRule>, IRulesRepository
    {
        public RulesRepository(AppDbContext context) : base(context) { }

        public async Task<BusinessHoursRule> GetRule(Guid id)
        {
            return await Context.Rules.AsNoTracking().Include(r => r.WorkHours).FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
