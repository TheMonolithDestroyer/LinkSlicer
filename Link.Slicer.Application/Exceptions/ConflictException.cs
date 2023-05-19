using System.Net;

namespace Link.Slicer.Application.Exceptions
{
    public class ConflictException : BaseException
    {
        public HttpStatusCode StatusCode = HttpStatusCode.Conflict;
        public ConflictException(string message) : base(message) { }
    }
}
