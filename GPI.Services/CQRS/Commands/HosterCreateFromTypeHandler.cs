using GPI.Core.Models.Entities;
using GPI.Services.ContentHosts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GPI.Services.CQRS.Commands
{
    public class HosterCreateFromTypeRequest : IRequest<IBasicContentHost> {
        public HosterCreateFromTypeRequest(string hosterTypeName)
        {
            TypeName = hosterTypeName;
        }

        public string TypeName { get; }
    }

    public class HosterCreateFromTypeHandler : IRequestHandler<HosterCreateFromTypeRequest, IBasicContentHost>
    {
        private readonly IMediator _mediator;
        private readonly IServiceProvider _serviceProvider;

        public HosterCreateFromTypeHandler(IMediator mediator, IServiceProvider serviceProvider)
        {
            this._mediator = mediator;
            this._serviceProvider = serviceProvider;
        }
        public async Task<IBasicContentHost> Handle(HosterCreateFromTypeRequest request, CancellationToken cancellationToken)
        {
            var hosterType = Type.GetType(request.TypeName, true);
            var hoster = (IBasicContentHost)_serviceProvider.AsSelf(hosterType);

            var settingsJson = await _mediator.Send(new HosterFetchConfigRequest(request.TypeName));
            hoster.LoadSettingsFromJson(settingsJson);
            return hoster;
        }
    }
}
