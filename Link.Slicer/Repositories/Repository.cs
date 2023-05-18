using Link.Slicer.Entities;
using Link.Slicer.Infrastructure.Persistence;
using Link.Slicer.Queries;
using Microsoft.EntityFrameworkCore;

namespace Link.Slicer.Repositories
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Url> _urls;
        public Repository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException();
            _urls = _context.Set<Url>() ?? throw new ArgumentNullException();
        }

        public async Task<Url> GetAsync(Query query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            var request = query as UrlFindQuery;
            if (request == null) throw new InvalidCastException(nameof(query));

            return await _urls
                .Where(i => !i.DeletedAt.HasValue && 
                        ((string.IsNullOrEmpty(request.DomainName) || (!string.IsNullOrEmpty(request.DomainName) && i.DomainName == request.DomainName)) &&
                        (string.IsNullOrEmpty(request.Address) || (!string.IsNullOrEmpty(request.Address) && i.Address == request.Address))))
                .OrderByDescending(i => i.CreatedAt)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public void Insert(Url entity)
        {
            _urls.Attach(entity);
            _context.Entry(entity).State = EntityState.Added;
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
