using Link.Slicer.Database;
using Link.Slicer.Entities;
using Link.Slicer.Models;
using Link.Slicer.Utils;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text.RegularExpressions;

namespace Link.Slicer.Services
{
    public interface IUrlService
    {
        Task<Result<string>> RedicrectAsync(HttpRequest request);
        Task<UrlCreateResponse> CreateAsync(UrlCreateRequest model);
    }

    public class UrlService : IUrlService
    {
        private readonly IOptions<AppSettings> _options;
        private readonly IRepository _repository;
        public UrlService(
            IOptions<AppSettings> options,
            IRepository repository)
        {
            _options = options ?? throw new ArgumentNullException();
            _repository = repository ?? throw new ArgumentNullException();
        }
        
        public async Task<Result<string>> RedicrectAsync(HttpRequest request)
        {
            var path = request.Path.Value?.Replace(@"/", "");
            var domainName = request.Host.Value;
            var protocol = request.Scheme;

            var urlEntity = await _repository.GetAsync(path);
            if (urlEntity == null)
                throw new Exception("There is no url found.");

            return Result.Succeed(urlEntity.Target, HttpStatusCode.Redirect);
        }

        public async Task<UrlCreateResponse> CreateAsync(UrlCreateRequest model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(UrlCreateRequest));

            if (string.IsNullOrEmpty(model.Address))
                throw new ArgumentNullException("Address can not be null or empty.", nameof(model.Address));

            model.DomainName = GetDomain(model.Target);

            var entity = await _repository.GetAsync(model.DomainName, model.Address, model.Target);
            if (entity == null)
            {
                entity = new Url
                {
                    CreatedAt = DateTimeOffset.Now.ToTimestamp(),
                    UpdatedAt = DateTimeOffset.Now.ToTimestamp(),
                    DomainName = model.DomainName,
                    Address = model.Address,
                    Target = model.Target,
                    Description = model.Description,
                    Protocol = GetProtocol(model.Target)
                };
                _repository.Insert(entity);
                _repository.Commit();
            }

            return new UrlCreateResponse
            {
                ShortUrl = GenerateShortUrl(entity.Protocol, entity.Address),
                Description = entity.Description,
                CreatedAt = entity.CreatedAt.ToDateTime()
            };
        }

        private string GetDomain(string target)
        {
            if (string.IsNullOrEmpty(target))
                throw new ArgumentNullException("Target url can not be null or empty.", nameof(target));

            var uri = new Uri(target);
            var domain = uri.Host;

            return domain;
        }

        private string GetProtocol(string target)
        {
            if (string.IsNullOrEmpty(target))
                throw new ArgumentNullException("Target url can not be null or empty.", nameof(target));

            var uri = new Uri(target);
            var protocol = uri.Scheme;

            return protocol;
        }

        private string AddProtocol(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("Url url can not be null or empty.", nameof(url));

            var hasProtocol = Regex.IsMatch(url, @"^\w+:\/\/");
            return hasProtocol ? url : $"http://{url}";
        }

        private string GenerateShortUrl(string protocol, string address)
        {
            var domain = GetDomain(_options.Value.DefaultDomain);
            return $"{protocol}://{domain}/{address}";
        }
    }
}
