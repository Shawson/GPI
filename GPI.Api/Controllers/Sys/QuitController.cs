using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GPI.Api.Controllers.Sys
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class QuitController : ControllerBase
    {
        private readonly ILogger<QuitController> _logger;

        public QuitController(ILogger<QuitController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public void Get()
        {
            System.Environment.Exit(0);
        }
    }
}