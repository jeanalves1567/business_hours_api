namespace BusinessHours.Domain.Dtos.Rules
{
    public class WorkHoursReadDto
    {
        public string Day { get; set; }
        public bool Open { get; set; }
        public string Start { get; set; }
        public string Finish { get; set; }
    }
}
