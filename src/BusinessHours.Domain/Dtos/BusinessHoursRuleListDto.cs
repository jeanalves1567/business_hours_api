using System;

namespace BusinessHours.Domain.Dtos
{
    public class BusinessHoursRuleListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Timezone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
