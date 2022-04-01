using BusinessHours.Domain.Dtos.Rules;

namespace BusinessHours.Domain.Dtos.Departments
{
    public class DepartmentMomentStatus
    {
        public bool AtWorkingHours { get; set; }
        public RuleReadDto Rule { get; set; }
    }
}
