using System;

namespace BusinessHours.Domain.Dtos.Holidays
{
    public class HolidayListDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public bool AllDay { get; set; }
        public string Start { get; set; }
        public string Finish { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
