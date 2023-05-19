using Link.Slicer.Application.Models;

namespace Link.Slicer.Application.Services.UrlService
{
    public interface IUrlService
    {
        Task<string> GetOriginUrl(string path);
        Task<Result> CreateAsync(CreateUrlCommandRequest command);
    }
}
