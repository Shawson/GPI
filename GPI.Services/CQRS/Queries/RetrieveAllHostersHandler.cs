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
    public class RetrieveAllHostersRequest : IRequest<List<Hoster>>
    {
    }

    public class RetrieveAllHostersHandler : IRequestHandler<RetrieveAllHostersRequest, List<Hoster>>
    {
        private readonly IRepository<Hoster> _hosterRepository;

        public RetrieveAllHostersHandler(IRepository<Hoster> hosterRepository)
        {
            this._hosterRepository = hosterRepository;
        }
        public async Task<List<Hoster>> Handle(RetrieveAllHostersRequest request, CancellationToken cancellationToken)
        {
            return await _hosterRepository
                .GetAllAsync()
                .ToListAsync();
        }
    }
}