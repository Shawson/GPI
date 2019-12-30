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
    public class RetrieveAllGamesRequest : IRequest<List<Game>>
    {
        public RetrieveAllGamesRequest(Guid gameId)
        {
            GameId = gameId;
        }

        public Guid GameId { get; }
    }

    public class RetrieveAllGamesHandler : IRequestHandler<RetrieveAllGamesRequest, List<Game>>
    {
        private readonly IRepository<Game> _gameRepository;

        public RetrieveAllGamesHandler(IRepository<Game> gameRepository)
        {
            this._gameRepository = gameRepository;
        }
        public async Task<List<Game>> Handle(RetrieveAllGamesRequest request, CancellationToken cancellationToken)
        {
            return await _gameRepository.GetAllAsync().ToListAsync();
        }
    }
}