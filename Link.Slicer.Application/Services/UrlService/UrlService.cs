using Link.Slicer.Application.Common.Helpers;
using Link.Slicer.Application.Common.Interfaces;
using Link.Slicer.Application.Exceptions;
using Link.Slicer.Application.Models;
using Link.Slicer.Application.Settings;
using Link.Slicer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;

namespace Link.Slicer.Application.Services.UrlService
{
    public record CreateUrlCommandRequest(string Shortening, string Url, string Comment);
    public record CreateUrlCommandResponse(string ShortUrl, string Comment, DateTimeOffset CreatedAt);

    public class UrlService : IUrlService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly IApplicationLogger<UrlService> _logger;
        private readonly IApplicationDbContext _context;
        public UrlService(
            IApplicationDbContext context,
            IOptions<AppSettings> settings,
            IApplicationLogger<UrlService> logger)
        {
            _context = context;
            _settings = settings;
            _logger = logger;
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


        public async Task<Result> CreateAsync(CreateUrlCommandRequest command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));
            if (string.IsNullOrEmpty(command.Url))
                throw new BadRequestException($"{nameof(command.Url)} обязательное поле.");
            if (string.IsNullOrEmpty(command.Shortening))
                throw new BadRequestException($"{nameof(command.Shortening)} обязательное поле.");

            var urlParts = UrlHelper.SplitUrl(command.Url);
            var similarEntityByShortening = await GetByShorteningAsync(command.Shortening);

            HttpStatusCode statusCode;
            if (similarEntityByShortening == null)
            {
                similarEntityByShortening = new Url
                {
                    Shortening = command.Shortening,
                    Protocol = urlParts["scheme"],
                    DomainName = urlParts["host"],
                    Address = urlParts["path"] + urlParts["query"],
                    Comment = command.Comment
                };
                Insert(similarEntityByShortening);

                statusCode = HttpStatusCode.Created;
            }
            else
            {
                var entityUrl = similarEntityByShortening.DomainName + similarEntityByShortening.Address;
                var incomingUrl = urlParts["host"] + urlParts["path"] + urlParts["query"];
                var similarUrlParts = entityUrl == incomingUrl;
                if (!similarUrlParts)
                    throw new ConflictException($"URL-адрес с данным сокращением \"{command.Shortening}\" уже существует. Пожалуйста, попробуйте использовать другое сокращение.");

                statusCode = HttpStatusCode.OK;
            }

            var response = new CreateUrlCommandResponse(
                UrlHelper.GenerateShortUrl(_settings.Value.DefaultDomain, similarEntityByShortening.Shortening),
                similarEntityByShortening.Comment,
                similarEntityByShortening.CreatedAt);

            return Result.Succeed(response, statusCode);
        }

        private void Insert(Url url)
        {
            _context.Urls.Add(url);
            _context.SaveChanges();

            _logger.LogInformation($"Url created: {JsonConvert.SerializeObject(url)}");
        }

        private async Task<Url> GetByShorteningAsync(string shortening)
        {
            return await _context.Urls
                .OrderByDescending(i => i.CreatedAt)
                .AsNoTracking()
                .FirstOrDefaultAsync(i => !i.DeletedAt.HasValue && i.Shortening == shortening);
        }
    }
}
