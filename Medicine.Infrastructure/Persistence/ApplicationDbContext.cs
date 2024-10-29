
using Medicine.Domain.Common;
using Medicine.Domain.Entities;
using Medicine.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Medicine.Infrastructure.Persistence;

public class ApplicationDbContext (DbContextOptions options, IHttpContextAccessor httpContextAccessor) : DbContext (options), IApplicationDbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public virtual DbSet<Medication> Medications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Medication>().HasQueryFilter(m => !m.IsDeleted);

        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        ChangeTracker.Entries<AuditableEntity>()
                     .Where(c => c.State == EntityState.Added)
                     .Select(entry =>
                     {
                         entry.Entity.CreationDate = DateTime.Now;
                         entry.Entity.CreatedBy = _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "Default User";
                         return entry;
                     });

        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker.Entries<AuditableEntity>()
                     .Where(c => c.State == EntityState.Added)
                     .Select(entry =>
                     {
                         entry.Entity.CreationDate = DateTime.Now;
                         entry.Entity.CreatedBy = _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "Default User";
                         return entry;
                     });

        return base.SaveChangesAsync(cancellationToken);
    }
    // Can add for Update . . .
}
