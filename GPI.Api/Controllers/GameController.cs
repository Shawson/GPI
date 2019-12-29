using System;
using System.Threading.Tasks;
using GPI.Core.Models.Entities;
using GPI.Services.CQRS.Commands;
using GPI.Services.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GPI.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private readonly IMediator _mediatr;

        public GameController(ILogger<GameController> logger, IMediator mediatr)
        {
            _logger = logger;
            _mediatr = mediatr;
        }

        [HttpGet]
        public async Task<ActionResult<Game>> Get(Guid gameId)
        {
            if (gameId == Guid.Empty)
            {
                return NotFound();
            }

            var game = await _mediatr.Send(new RetrieveGameRequest(gameId));
            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Game game)
        {
            game.Id = Guid.Empty;

            _ = await _mediatr.Send(new UpsertGameRequest(game));

            return Created($"Get/{game.Id}", game);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Game game)
        {
            if (game.Id == Guid.Empty)
            {
                return NotFound();
            }

            _ = await _mediatr.Send(new UpsertGameRequest(game));
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid gameId)
        {
            if (gameId == Guid.Empty)
            {
                return NotFound();
            }

            _ = await _mediatr.Send(new DeleteGameRequest(gameId));

            return Ok();
        }
    }
}
