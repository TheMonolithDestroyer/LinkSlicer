using System.Net;

namespace Link.Slicer.Application.Exceptions
{
    public class NotFoundException : BaseException
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NotFound;
        public NotFoundException(string message) : base (message)
        {
        }
    }
}
