using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Slicer.Application.Services.UrlService
{
    public interface IUrlService
    {
        Task<string> GetOriginUrl(string path);
        Task<CreateUrlCommandResponse> CreateAsync(CreateUrlCommandRequest command);
    }
}
