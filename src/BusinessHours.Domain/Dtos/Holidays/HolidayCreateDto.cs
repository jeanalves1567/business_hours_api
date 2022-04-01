using System.ComponentModel.DataAnnotations;

namespace BusinessHours.Domain.Dtos.Holidays
{
    public class HolidayCreateDto
    {
        [Required]
        public string Name { get; set; }

        public int? Year { get; set; }

        [Required]
        public int Month { get; set; }

        [Required]
        public int Day { get; set; }

        [Required]
        public bool AllDay { get; set; }

        public string Start { get; set; }

        public string Finish { get; set; }
    }
}
