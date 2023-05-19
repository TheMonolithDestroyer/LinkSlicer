using Link.Slicer.Application.Services.UrlService;
using Microsoft.AspNetCore.Mvc;

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
            var result = await _service.CreateAsync(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet]
        [Route("/{shortening}")]
        public async Task<IActionResult> Redirect()
        {
            return Redirect(await _service.GetOriginUrl(Request.Path));
        }
    }
}
