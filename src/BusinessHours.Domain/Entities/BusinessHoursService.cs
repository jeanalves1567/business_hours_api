using System;

namespace BusinessHours.Domain.Entities
{
    public class BusinessHoursService
    {
        public Guid RuleId { get; set; }
        public string Service { get; set; }
        public BusinessHoursRule Rule { get; set; }
    }
}
