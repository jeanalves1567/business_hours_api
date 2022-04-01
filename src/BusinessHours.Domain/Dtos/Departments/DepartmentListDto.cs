using System;

namespace BusinessHours.Domain.Dtos.Departments
{
    public class DepartmentListDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ExternalId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
