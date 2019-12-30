using GPI.Core.Models.DTOs;
using System.Collections.Generic;
using System.Threading;

namespace GPI.Api.BackgroundServices
{
    public interface IBackgroundTaskProgressTracker
    {
        List<BackgroundTaskStatus> GetActiveBackgroundTasks();
        void UpdateTask(string jobName, decimal progress);
        void AddTaskToTrack(string jobName, CancellationToken token);
    }
}