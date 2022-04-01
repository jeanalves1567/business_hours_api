using System;
using BusinessHours.Domain.Dtos.Rules;

namespace BusinessHours.Domain.Dtos.Departments
{
    public class DepartmentReadDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ExternalId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string RuleId { get; set; }
        public RuleListDto Rule { get; set; }
    }
}
