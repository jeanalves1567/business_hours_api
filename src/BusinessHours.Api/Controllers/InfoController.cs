using System;
using BusinessHours.Api.Models.Info;
using BusinessHours.Api.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BusinessHours.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public InfoController(IConfiguration configurarion, IWebHostEnvironment env)
        {
            _configuration = configurarion;
            _env = env;
        }

        [ProducesResponseType(typeof(ApiInfo), 200)]
        [HttpGet]
        public ActionResult<ApiInfo> GetApiInfo()
        {
            var result = new ApiInfo();
            result.Name = _configuration.GetValue<string>("AppName");
            result.Version = _configuration.GetValue<string>("Version");
            result.Environment = _env.EnvironmentName;
            result.TenantId = _configuration.GetValue<string>("TenantId");
            result.InstanceId = AppGlobals.InstanceId.ToString();
            result.ServerTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            result.Timezone = TimeZoneInfo.Local;
            return Ok(result);
        }
    }
}
