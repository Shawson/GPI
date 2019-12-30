using GPI.Core.Models.Entities;
using GPI.Data.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GPI.Services.CQRS.Queries
{
    public class RetrieveGameRequest : IRequest<Game>
    {
        public RetrieveGameRequest(Guid gameId)
        {
            GameId = gameId;
        }

        public Guid GameId { get; }
    }

    public class RetrieveGameHandler : IRequestHandler<RetrieveGameRequest, Game>
    {
        private readonly IRepository<Game> _gameRepository;

        public RetrieveGameHandler(IRepository<Game> gameRepository)
        {
            this._gameRepository = gameRepository;
        }
        public async Task<Game> Handle(RetrieveGameRequest request, CancellationToken cancellationToken)
        {
            return await _gameRepository.GetByIdAsync(request.GameId);
        }
    }
}