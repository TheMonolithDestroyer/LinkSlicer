using Link.Slicer.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace Link.Slicer.Infrastructure.Logging
{
    public class ApplicationLogger<T> : IApplicationLogger<T>
    {
        private readonly ILogger<T> _log;
        public ApplicationLogger(ILogger<T> log)
        {
            _log = log;
        }

        public void LogInformation(string message, Exception inner = null)
        {
            _log.LogInformation(message, inner);
        }

        public void LogWarning(string message, Exception inner = null)
        {
            _log.LogWarning(message, inner);
        }

        public void LogError(string message, Exception inner = null)
        {
            _log.LogError(message, inner);
        }
    }
}
