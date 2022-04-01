using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessHours.Domain.Dtos.Rules
{
    public class RuleCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Timezone { get; set; }

        public List<WorkHoursCreateDto> WorkHours { get; set; }
    }
}
