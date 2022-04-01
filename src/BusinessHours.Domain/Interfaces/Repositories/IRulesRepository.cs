using System.Threading.Tasks;
using BusinessHours.Domain.Entities;

namespace BusinessHours.Domain.Interfaces.Repositories
{
    public interface IRulesRepository : IRepository<BusinessHoursRule>
    {
        Task<BusinessHoursRule> GetRule(string id);
    }
}
