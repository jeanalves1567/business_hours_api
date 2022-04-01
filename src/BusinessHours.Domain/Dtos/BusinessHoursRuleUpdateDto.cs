using System.Collections.Generic;

namespace BusinessHours.Domain.Dtos
{
    public class BusinessHoursRuleUpdateDto
    {
        public string Name { get; set; }
        public string Timezone { get; set; }
        public List<WorkHoursUpdateDto> WorkHours { get; set; }
    }
}
