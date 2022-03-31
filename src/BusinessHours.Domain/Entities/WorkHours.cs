using System;

namespace BusinessHours.Domain.Entities
{
    public class WorkHours
    {
        public Guid RuleId { get; set; }

        public string Day { get; set; }
        public bool Open { get; set; }
        public string Start { get; set; }
        public string StartUtc { get; set; }
        public string Finish { get; set; }
        public string FinishUtc { get; set; }

        public BusinessHoursRule Rule { get; set; }
    }
}
