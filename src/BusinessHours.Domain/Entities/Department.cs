namespace BusinessHours.Domain.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string ExternalId { get; set; }
        public string RuleId { get; set; }
        public BusinessHoursRule Rule { get; set; }
    }
}
