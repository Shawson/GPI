using GPI.Core.Models.Entities;
using GPI.Data.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GPI.Services.CQRS.Commands
{
    public class GameBulkInsertRequest : IRequest<Unit>
    {
        public GameBulkInsertRequest(List<Game> games)
        {
            Game = games;
        }

        public List<Game> Game { get; }
    }
    public class GameBulkInsertHandler : IRequestHandler<GameBulkInsertRequest, Unit>
    {
        private readonly IRepository<Game> _gameRepository;

        public GameBulkInsertHandler(IRepository<Game> gameRepository)
        {
            this._gameRepository = gameRepository;
        }

        public async Task<Unit> Handle(GameBulkInsertRequest request, CancellationToken cancellationToken)
        {
            await _gameRepository.AddRangeAsync(request.Game);
            await _gameRepository.SaveChanges();

            return Unit.Value;
        }
    }
}
