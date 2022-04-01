namespace BusinessHours.Domain.Dtos.Holidays
{
    public class HolidayUpdateDto
    {
        public string Name { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }
        public bool? AllDay { get; set; }
        public string Start { get; set; }
        public string Finish { get; set; }
    }
}
