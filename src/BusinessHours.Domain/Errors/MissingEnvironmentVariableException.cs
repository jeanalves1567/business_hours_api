using System;

namespace BusinessHours.Domain.Errors
{
    public class MissingEnvironmentVariableException : Exception
    {
        public MissingEnvironmentVariableException(string missingEnvironmentVariable) : base($"Missing Environment variable: {missingEnvironmentVariable}") { }
    }
}
