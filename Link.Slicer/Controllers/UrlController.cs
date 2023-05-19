using Link.Slicer.Application.Models;
using Link.Slicer.Application.Services.UrlService;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Link.Slicer.Controllers
{
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IUrlService _service;
        public UrlController(IUrlService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> Create([FromBody]CreateUrlCommandRequest request)
        {
            var result = Result.Succeed(await _service.CreateAsync(request), HttpStatusCode.Created);
            return StatusCode((int)HttpStatusCode.Created, result);
        }

        [HttpGet]
        [Route("/{shortening}")]
        public async Task<IActionResult> Redirect()
        {
            return Redirect(await _service.GetOriginUrl(Request.Path));
        }
    }
}
