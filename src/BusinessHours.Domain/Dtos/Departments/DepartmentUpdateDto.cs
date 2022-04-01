namespace BusinessHours.Domain.Dtos.Departments
{
    public class DepartmentUpdateDto
    {
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string RuleId { get; set; }
    }
}
