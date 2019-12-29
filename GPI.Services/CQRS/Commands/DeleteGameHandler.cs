using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GPI.Services.CQRS.Commands
{
    public class DeleteGameRequest : IRequest<Unit>
    {
        public DeleteGameRequest(Guid gameId)
        {
            GameId = gameId;
        }

        public Guid GameId { get; }
    }

    public class DeleteGameHandler : IRequestHandler<DeleteGameRequest, Unit>
    {
        public DeleteGameHandler()
        {

        }
        public Task<Unit> Handle(DeleteGameRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
