using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GPI.Services.BackgroundTasks
{
    public interface IBackgroundMediatrTaskQueue
    {
        void QueueBackgroundWorkItem(IRequest workItem);

        Task<IRequest> DequeueAsync(
            CancellationToken cancellationToken);
    }
}
