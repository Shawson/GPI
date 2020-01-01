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
    public class HosterGetAllRequest : IRequest<List<Hoster>>
    {
    }

    public class HosterGetAllHandler : IRequestHandler<HosterGetAllRequest, List<Hoster>>
    {
        private readonly IRepository<Hoster> _hosterRepository;

        public HosterGetAllHandler(IRepository<Hoster> hosterRepository)
        {
            this._hosterRepository = hosterRepository;
        }
        public async Task<List<Hoster>> Handle(HosterGetAllRequest request, CancellationToken cancellationToken)
        {
            return await _hosterRepository
                .GetAllAsync()
                .ToListAsync();
        }
    }
}