namespace Link.Slicer.Application.Common.Interfaces
{
    /// <summary>
    /// Custom wrapper for ILogger
    /// </summary>
    public interface IApplicationLogger<T>
    {
        /// <summary>
        /// Logs information level message
        /// </summary>
        void LogInformation(string message, Exception inner = null);
        /// <summary>
        /// Logs warning level message
        /// </summary>
        void LogWarning(string message, Exception inner = null);
        /// <summary>
        /// Logs error level message
        /// </summary>
        void LogError(string message, Exception inner = null);
    }
}
