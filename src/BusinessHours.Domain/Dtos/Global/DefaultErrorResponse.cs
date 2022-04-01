namespace BusinessHours.Domain.Dtos.Global
{
    public class DefaultErrorResponse
    {
        public string Message { get; set; }

        public DefaultErrorResponse(string message)
        {
            Message = message;
        }
    }
}
