using GPI.Core.Models.Entities;
using GPI.Data.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GPI.Services.CQRS.Queries
{
    public class GameGetByIdRequest : IRequest<Game>
    {
        public GameGetByIdRequest(Guid gameId)
        {
            GameId = gameId;
        }

        public Guid GameId { get; }
    }

    public class GameGetByIdHandler : IRequestHandler<GameGetByIdRequest, Game>
    {
        private readonly IRepository<Game> _gameRepository;

        public GameGetByIdHandler(IRepository<Game> gameRepository)
        {
            this._gameRepository = gameRepository;
        }
        public async Task<Game> Handle(GameGetByIdRequest request, CancellationToken cancellationToken)
        {
            return await _gameRepository.GetByIdAsync(request.GameId);
        }
    }
}