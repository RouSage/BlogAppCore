using System.Threading;
using System.Threading.Tasks;
using BlogAppCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogAppCore.Application.Interfaces
{
    public interface IBlogAppCoreDbContext
    {
        DbSet<Post> Posts { get; set; }

        DbSet<Category> Categories { get; set; }

        DbSet<Tag> Tags { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}