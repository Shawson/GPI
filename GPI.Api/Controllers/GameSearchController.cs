using System.Collections.Generic;
using System.Threading.Tasks;
using GPI.Core.Models.DTOs;
using GPI.Core.Models.Entities;
using GPI.Services.CQRS.Queries;
using MediatR;
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
        private readonly IMediator _mediator;

        public GameSearchController(
            ILogger<GameSearchController> logger,
            IMediator mediator)
        {
            _logger = logger;
            this._mediator = mediator;
        }

        [HttpGet("")]
        public async Task<IEnumerable<Game>> Get()
        {
            return await Get(50, 1);
        }

        [HttpGet("{pageSize}-{currentPage}")]
        public async Task<IEnumerable<Game>> Get(int pageSize, int currentPage)
        {
            return await _mediator.Send(new GameGetAllRequest());
        }

        [HttpPost]
        public IEnumerable<Game> Post([FromBody]GameSearchOptions options, int pageSize, int currentPage)
        {
            return new List<Game> { new Game() };
        }

    }
}