using System.ComponentModel.DataAnnotations;

namespace BusinessHours.Domain.Dtos.Departments
{
    public class DepartmentCreateDto
    {
        [Required]
        public string ExternalId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        public string RuleId { get; set; }
    }
}
