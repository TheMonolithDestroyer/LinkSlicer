namespace Link.Slicer.Application.Common.Interfaces
{
    public interface IApplicationLogger<T>
    {
        void LogInformation(string message, Exception inner = null);
        void LogWarning(string message, Exception inner = null);
        void LogError(string message, Exception inner = null);
    }
}
