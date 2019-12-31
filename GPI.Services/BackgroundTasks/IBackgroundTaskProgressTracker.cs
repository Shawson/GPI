using GPI.Core.Models.DTOs;
using System.Collections.Generic;
using System.Threading;

namespace GPI.Services.BackgroundTasks
{
    public interface IBackgroundTaskProgressTracker
    {
        List<BackgroundTaskStatus> GetActiveBackgroundTasks();
        void AddTaskToTrack(string jobName, CancellationToken token);
        void UpdateTask(string jobName, decimal progress);
        void MarkTaskFinished(string jobName);
        
    }
}