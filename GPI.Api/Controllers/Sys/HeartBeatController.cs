using Microsoft.AspNetCore.Mvc;

namespace GPI.Api.Controllers.Sys
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class HeartBeatController : ControllerBase
    {
        [HttpGet]
        public bool Get()
        {
            return true;
        }
    }
}