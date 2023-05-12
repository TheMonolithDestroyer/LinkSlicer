using Link.Slicer.Models;

namespace Link.Slicer.Services
{
    public interface IUrlService
    {
        Task<string> RedicrectAsync(HttpRequest request);
        Task<UrlCreateResponse> CreateAsync(UrlCreateRequest model);
    }
}
