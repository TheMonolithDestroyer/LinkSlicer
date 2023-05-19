using Link.Slicer.Application.Common.Helpers;
using Link.Slicer.Application.Common.Interfaces;
using Link.Slicer.Application.Exceptions;
using Link.Slicer.Application.Models;
using Link.Slicer.Application.Settings;
using Link.Slicer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;

namespace Link.Slicer.Application.Services.UrlService
{
    public record CreateUrlCommandRequest(string Shortening, string Url, string Comment);
    public record CreateUrlCommandResponse(string ShortUrl, string Comment, DateTimeOffset CreatedAt);

    public class UrlService : IUrlService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly IApplicationDbContext _context;
        public UrlService(
            IApplicationDbContext context,
            IOptions<AppSettings> settings)
        {
            _context = context;
            _settings = settings;
        }

        public async Task<string> GetOriginUrl(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new NotFoundException("Недействительный адрес.");

            var shortening = path.Replace(@"/", "");
            var urlEntity = await _context.Urls
                .Where(i => !i.DeletedAt.HasValue && i.Shortening == shortening)
                .OrderByDescending(i => i.CreatedAt)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (urlEntity == null)
                throw new NotFoundException("Контент или ресурс не найден.");

            var originalUrl = UrlHelper.GenerateLongUrl(urlEntity.Protocol, urlEntity.DomainName, urlEntity.Address);
            return originalUrl;
        }


        public async Task<CreateUrlCommandResponse> CreateAsync(CreateUrlCommandRequest command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));
            if (string.IsNullOrEmpty(command.Url))
                throw new ArgumentNullException(nameof(command.Url));
            if (string.IsNullOrEmpty(command.Shortening))
                throw new ArgumentNullException(nameof(command.Shortening));

            // Валидация, если адрес другой, но домен и таргет одинаковые

            var urlParts = UrlHelper.SplitUrl(command.Url);
            var entity = await _context.Urls
                .Where(i => !i.DeletedAt.HasValue &&
                        (string.IsNullOrEmpty(urlParts["host"]) || !string.IsNullOrEmpty(urlParts["host"]) && i.DomainName == urlParts["host"]) &&
                        (string.IsNullOrEmpty(command.Shortening) || !string.IsNullOrEmpty(command.Shortening) && i.Address == command.Shortening))
                .OrderByDescending(i => i.CreatedAt)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                entity = new Url
                {
                    Shortening = command.Shortening,
                    Protocol = urlParts["scheme"],
                    DomainName = urlParts["host"],
                    Address = urlParts["path"] + urlParts["query"],
                    Comment = command.Comment
                };

                _context.Urls.Add(entity);
                _context.SaveChanges();
            }

            var shortUrl = UrlHelper.GenerateShortUrl(_settings.Value.DefaultDomain, entity.Shortening);
            return new CreateUrlCommandResponse(shortUrl, entity.Comment, entity.CreatedAt);
        }
    }
}
