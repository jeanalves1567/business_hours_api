using System.Collections.Generic;

namespace BusinessHours.Domain.Entities
{
    public class Holiday : BaseEntity
    {
        public string Name { get; set; }
        public int? Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public bool AllDay { get; set; }
        public string Start { get; set; }
        public string Finish { get; set; }

        public IEnumerable<RuleHoliday> Rules { get; set; }
    }
}
