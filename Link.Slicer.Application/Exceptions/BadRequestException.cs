using System.Net;

namespace Link.Slicer.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public HttpStatusCode StatusCode = HttpStatusCode.BadRequest;
        public BadRequestException(string message) : base(message) { }
    }
}
