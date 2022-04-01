namespace BusinessHours.Domain.Errors
{
    public class InvalidBodyParamException : BadRequestException
    {
        public InvalidBodyParamException(string param) : base($"Invalid body param: {param}") { }
    }
}
