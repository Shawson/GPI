using GPI.Core.Models.DTOs;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace GPI.Api.BackgroundServices
{
    public class BackgroundTaskProgressTracker : IBackgroundTaskProgressTracker
    {
        private ConcurrentDictionary<string, decimal> _workUnderway = new ConcurrentDictionary<string, decimal>();

        public void ReportWork(string jobName, decimal progress)
        {
            if (progress == 1)
            {
                _workUnderway.TryRemove(jobName, out _);
            }
            else
            {
                _workUnderway.AddOrUpdate(jobName, progress, (key, oldValue) => progress);
            }
        }

        public List<BackgroundTaskStatus> GetActiveBackgroundTasks()
        {
            return _workUnderway
                .ToArray()
                .Select(x => new BackgroundTaskStatus()
                {
                    Name = x.Key,
                    Progress = x.Value
                })
                .ToList();
        }
    }
}
