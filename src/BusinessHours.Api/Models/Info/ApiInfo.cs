using System;

namespace BusinessHours.Api.Models.Info
{
    public class ApiInfo
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string TenantId { get; set; }
        public string InstanceId { get; set; }
        public string ServerTime { get; set; }
        public TimeZoneInfo Timezone { get; set; }
    }
}
