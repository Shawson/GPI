using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GPI.Services.CQRS.Commands
{
    public class GameDeleteRequest : IRequest<Unit>
    {
        public GameDeleteRequest(Guid gameId)
        {
            GameId = gameId;
        }

        public Guid GameId { get; }
    }

    public class GameDeleteHandler : IRequestHandler<GameDeleteRequest, Unit>
    {
        public GameDeleteHandler()
        {

        }
        public Task<Unit> Handle(GameDeleteRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
