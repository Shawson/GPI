using System.Collections.Generic;
using GPI.Core.Models.DTOs;
using GPI.Core.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GPI.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class GameSearchController : ControllerBase
    {
        private readonly ILogger<GameSearchController> _logger;

        public GameSearchController(ILogger<GameSearchController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/")]
        public IEnumerable<Game> Get()
        {
            return Get(50, 1);
        }

        [HttpGet]
        [Route("/{pageSize}-{currentPage}")]
        public IEnumerable<Game> Get(int pageSize, int currentPage)
        {
            return new List<Game> { new Game() };
        }

        [HttpPost]
        public IEnumerable<Game> Post([FromBody]GameSearchOptions options, int pageSize, int currentPage)
        {
            return new List<Game> { new Game() };
        }

    }
}