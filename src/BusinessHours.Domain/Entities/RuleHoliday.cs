namespace BusinessHours.Domain.Entities
{
    public class RuleHoliday
    {
        public string RuleId { get; set; }
        public string HolidayId { get; set; }
        public BusinessHoursRule Rule { get; set; }
        public Holiday Holiday { get; set; }
    }
}
