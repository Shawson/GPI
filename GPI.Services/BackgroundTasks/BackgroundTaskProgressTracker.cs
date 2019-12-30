using GPI.Core.Models.DTOs;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GPI.Services.BackgroundTasks
{
    public class BackgroundTaskProgressTracker : IBackgroundTaskProgressTracker
    {
        private ConcurrentDictionary<string, decimal> _workUnderway = new ConcurrentDictionary<string, decimal>();
        private ConcurrentDictionary<string, CancellationToken> _tokenStore = new ConcurrentDictionary<string, CancellationToken>();

        public void AddTaskToTrack(string jobName, CancellationToken token)
        {
            _workUnderway.AddOrUpdate(jobName, 0, (key, oldValue) => 0);
            _tokenStore.AddOrUpdate(jobName, token, (key, oldValue) => token);
        }

        public void UpdateTask(string jobName, decimal progress)
        {
            if (progress == 1)
            {
                _workUnderway.TryRemove(jobName, out _);
                _tokenStore.TryRemove(jobName, out _);
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
