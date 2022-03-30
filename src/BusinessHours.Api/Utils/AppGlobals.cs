using System;

namespace BusinessHours.Api.Utils
{
    public static class AppGlobals
    {
        public static Guid InstanceId { get; set; } = Guid.NewGuid();
    }
}
