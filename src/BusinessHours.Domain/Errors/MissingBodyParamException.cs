namespace BusinessHours.Domain.Errors
{
    public class MissingBodyParamException : BadRequestException
    {
        public MissingBodyParamException(string param) : base($"Missing required body param: {param}") { }
    }
}
