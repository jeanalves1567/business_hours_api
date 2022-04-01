using System.ComponentModel.DataAnnotations;

namespace BusinessHours.Domain.Dtos.Rules
{
    public class WorkHoursUpdateDto
    {
        [Required]
        public string Day { get; set; }

        [Required]
        public bool Open { get; set; }

        public string Start { get; set; }
        public string Finish { get; set; }
    }
}
