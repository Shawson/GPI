using GPI.Core.Models.Entities;
using GPI.Data.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GPI.Services.CQRS.Queries
{
    public class GameGetAllRequest : IRequest<List<Game>>
    {
        public GameGetAllRequest(Guid gameId)
        {
            GameId = gameId;
        }

        public Guid GameId { get; }
    }

    public class GameGetAllHandler : IRequestHandler<GameGetAllRequest, List<Game>>
    {
        private readonly IRepository<Game> _gameRepository;

        public GameGetAllHandler(IRepository<Game> gameRepository)
        {
            this._gameRepository = gameRepository;
        }
        public async Task<List<Game>> Handle(GameGetAllRequest request, CancellationToken cancellationToken)
        {
            return await _gameRepository.GetAllAsync().ToListAsync();
        }
    }
}