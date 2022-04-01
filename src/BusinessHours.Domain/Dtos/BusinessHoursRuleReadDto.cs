using System;
using System.Collections.Generic;

namespace BusinessHours.Domain.Dtos
{
    public class BusinessHoursRuleReadDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Timezone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public IEnumerable<WorkHoursReadDto> WorkHours { get; set; }
    }
}
