using Link.Slicer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Slicer.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Url> Urls { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        void InsertUrl(Url entity);
        Task<Url> GetByShorteningAsync(string shortening);
    }
}
