using System;

namespace BusinessHours.Domain.Dtos.Rules
{
    public class RuleListDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Timezone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
