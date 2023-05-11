using Link.Slicer.Models;
using Link.Slicer.Services;
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
        public async Task<IActionResult> Create([FromBody]UrlCreateRequest request)
        {
            var result = await _service.CreateAsync(request);
            return StatusCode((int)HttpStatusCode.Created, Result.Succeed(result, HttpStatusCode.Created));
        }

        [HttpGet]
        [Route("/s7")]
        public async Task<IActionResult> RedirectTo()
        {
            var result = await _service.RedicrectAsync(Request);

            return Redirect(result.Data);
        }
    }
}
