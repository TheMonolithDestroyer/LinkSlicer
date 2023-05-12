using Link.Slicer.Entities;
using Link.Slicer.Models;
using Link.Slicer.Queries;
using Link.Slicer.Repositories;
using Link.Slicer.Utils;
using Microsoft.Extensions.Options;

namespace Link.Slicer.Services
{
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
        
        public async Task<string> RedicrectAsync(HttpRequest request)
        {
            var address = request.Path.Value?.Replace(@"/", "");
            var query = new UrlFindQuery { Address = address };

            var entity = await _repository.GetAsync(query);
            if (entity == null)
                throw new Exception("There is no url found.");

            return entity.Target;
        }

        public async Task<UrlCreateResponse> CreateAsync(UrlCreateRequest model)
        {
            if (model == null) 
                throw new ArgumentNullException(string.Format(Resource.CanNotBeNull, nameof(model)));
            if (string.IsNullOrEmpty(model.Url)) 
                throw new ArgumentNullException(string.Format(Resource.CanNotBeNull, nameof(model.Url)));
            if (string.IsNullOrEmpty(model.Shortening)) 
                throw new ArgumentNullException(string.Format(Resource.CanNotBeNull, nameof(model.Shortening)));

            var domainName = UrlHelper.GetDomain(model.Url);
            var query = new UrlFindQuery
            {
                Address = model.Shortening,
                DomainName = domainName
            };

            var entity = await _repository.GetAsync(query);
            if (entity == null)
            {
                entity = new Url
                {
                    CreatedAt = DateTimeOffset.Now.ToTimestamp(),
                    UpdatedAt = DateTimeOffset.Now.ToTimestamp(),
                    DomainName = domainName,
                    Address = model.Shortening,
                    Target = model.Url,
                    Description = model.Comment,
                    Protocol = UrlHelper.GetScheme(model.Url)
                };
                _repository.Insert(entity);
                _repository.Commit();
            }

            return new UrlCreateResponse
            {
                ShortUrl = UrlHelper.GenerateShortUrl(_options.Value.DefaultDomain, entity.Address),
                Description = entity.Description,
                CreatedAt = entity.CreatedAt.ToDateTime()
            };
        }
    }
}
