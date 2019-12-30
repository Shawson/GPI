using GPI.Core.Models.DTOs;
using System.Collections.Generic;

namespace GPI.Api.BackgroundServices
{
    public interface IBackgroundTaskProgressTracker
    {
        List<BackgroundTaskStatus> GetActiveBackgroundTasks();
        void ReportWork(string jobName, decimal progress);
    }
}