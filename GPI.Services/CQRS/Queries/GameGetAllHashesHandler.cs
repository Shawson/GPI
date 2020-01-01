using GPI.Core.Models.Entities;
using GPI.Data.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GPI.Services.CQRS.Queries
{
    public class GameGetAllHashesRequest : IRequest<List<string>>
    {
    }

    public class GameGetAllHashesHandler : IRequestHandler<GameGetAllHashesRequest, List<string>>
    {
        private readonly IRepository<Game> _gameRepository;

        public GameGetAllHashesHandler(IRepository<Game> gameRepository)
        {
            this._gameRepository = gameRepository;
        }
        public async Task<List<string>> Handle(GameGetAllHashesRequest request, CancellationToken cancellationToken)
        {
            return await _gameRepository
                .GetAllAsync()
                .Select(x => x.Hash)
                .ToListAsync();
        }
    }
}