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
    public class InsertBulkGamesRequest : IRequest<Unit>
    {
        public InsertBulkGamesRequest(List<Game> games)
        {
            Game = games;
        }

        public List<Game> Game { get; }
    }
    public class InsertBulkGamesHandler : IRequestHandler<InsertBulkGamesRequest, Unit>
    {
        private readonly IRepository<Game> _gameRepository;

        public InsertBulkGamesHandler(IRepository<Game> gameRepository)
        {
            this._gameRepository = gameRepository;
        }

        public async Task<Unit> Handle(InsertBulkGamesRequest request, CancellationToken cancellationToken)
        {
            await _gameRepository.AddRangeAsync(request.Game);
            await _gameRepository.SaveChanges();

            return Unit.Value;
        }
    }
}
