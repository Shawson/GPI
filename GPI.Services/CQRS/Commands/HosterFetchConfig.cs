using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GPI.Services.CQRS.Commands
{
    public class HosterFetchConfigRequest : IRequest<string>
    {
        public string TypeName { get; set; }

        public HosterFetchConfigRequest(string typeName)
        {
            TypeName = typeName;
        }
    }

    public class HosterFetchConfigHandler : IRequestHandler<HosterFetchConfigRequest, string>
    {
        public async Task<string> Handle(HosterFetchConfigRequest request, CancellationToken cancellationToken)
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            var finalPath = Path.Combine(path, $"Config/ContentScanners/{request.TypeName}.json");

            return File.ReadAllText(finalPath);
        }
    }
}
