using Link.Slicer.Entities;
using Link.Slicer.Queries;

namespace Link.Slicer.Repositories
{
    public interface IRepository
    {
        Task<Url> GetAsync(Query query);
        void Insert(Url entity);
        int Commit();
        Task<int> CommitAsync();
    }
}
