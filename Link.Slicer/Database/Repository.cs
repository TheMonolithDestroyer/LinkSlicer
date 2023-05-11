using Link.Slicer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Link.Slicer.Database
{
    public interface IRepository
    {
        Task<Url> GetAsync(string address);
        Task<Url> GetAsync(string domain, string address, string target);
        void Insert(Url entity);
        public int Commit();
        Task<int> CommitAsync();
    }

    public class Repository : IRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Url> _urls;
        public Repository(AppDbContext context)
        {
            _context = context;
            _urls = _context.Set<Url>() ?? throw new ArgumentNullException();
        }

        public async Task<Url> GetAsync(string address)
        {
            var result = await _urls.FirstOrDefaultAsync(i =>
                !i.DeletedAt.HasValue &&
                i.Address == address);

            return result;
        }

        public async Task<Url> GetAsync(string domain, string address, string target)
        {
            var result = await _urls.FirstOrDefaultAsync(i => 
                !i.DeletedAt.HasValue && 
                i.DomainName == domain && 
                i.Address == address 
                && i.Target == target);

            return result;
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
