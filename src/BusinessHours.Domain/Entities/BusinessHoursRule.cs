using System.Collections.Generic;

namespace BusinessHours.Domain.Entities
{
    public class BusinessHoursRule : BaseEntity
    {
        public string Name { get; set; }
        public string Timezone { get; set; }
        public IEnumerable<WorkHours> WorkHours { get; set; }
    }
}
