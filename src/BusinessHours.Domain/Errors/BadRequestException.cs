using System;

namespace BusinessHours.Domain.Errors
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}
