using System.Threading.Tasks;

namespace GPI.Api.AppStart
{
    internal interface IApplicationStartWorker
    {
        Task DoWork();
    }
}
