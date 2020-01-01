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
    public class RetrieveAllGameHashesRequest : IRequest<List<string>>
    {
    }

    public class RetrieveAllGameHashesHandler : IRequestHandler<RetrieveAllGameHashesRequest, List<string>>
    {
        private readonly IRepository<Game> _gameRepository;

        public RetrieveAllGameHashesHandler(IRepository<Game> gameRepository)
        {
            this._gameRepository = gameRepository;
        }
        public async Task<List<string>> Handle(RetrieveAllGameHashesRequest request, CancellationToken cancellationToken)
        {
            return await _gameRepository
                .GetAllAsync()
                .Select(x => x.Hash)
                .ToListAsync();
        }
    }
}